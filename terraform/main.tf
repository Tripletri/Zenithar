## Provider
terraform {
  required_providers {
    yandex = {
      source = "yandex-cloud/yandex"
    }
  }
  required_version = ">= 0.13"
}

provider "yandex" {
  zone      = "ru-central1-a"
  token     = var.yandex_iam_token
  cloud_id  = var.cloud_id
  folder_id = var.folder_id
}

## Network
resource "yandex_vpc_network" "prod-net" {
  name = "prod-net"
}

resource "yandex_vpc_subnet" "subnet1" {
  name           = "subnet1"
  zone           = "ru-central1-a"
  network_id     = yandex_vpc_network.prod-net.id
  v4_cidr_blocks = ["192.168.10.0/24"]
}

## Postgres
resource "yandex_mdb_postgresql_cluster" "zenithar-pgsql" {
  name        = "zenithar-pgsql"
  environment = var.production_mode ? "PRODUCTION" : "PRESTABLE"
  network_id  = yandex_vpc_network.prod-net.id

  config {
    version = "15"

    resources {
      resource_preset_id = "b1.medium"
      disk_type_id       = "network-hdd"
      disk_size          = "10"
    }

    access {
      web_sql = true
    }
  }

  database {
    name  = var.pg_db
    owner = var.pg_username
  }

  user {
    name     = var.pg_username
    password = var.pg_password
    permission {
      database_name = var.pg_db
    }
  }

  host {
    zone             = "ru-central1-a"
    subnet_id        = yandex_vpc_subnet.subnet1.id
    assign_public_ip = !var.production_mode
  }
}

output "pg_fqdn" {
  value = yandex_mdb_postgresql_cluster.zenithar-pgsql.host.0.fqdn
}

## SA static key
resource "yandex_iam_service_account_static_access_key" "sa-static-key" {
  service_account_id = var.service_account_id
  description        = "static access key for object storage"
}

## Object storage
resource "yandex_storage_bucket" "bucket" {
  access_key = yandex_iam_service_account_static_access_key.sa-static-key.access_key
  secret_key = yandex_iam_service_account_static_access_key.sa-static-key.secret_key

  bucket = var.bucket_name
  acl    = "public-read"
}

output "bucket_domain_name" {
  value = yandex_storage_bucket.bucket.bucket_domain_name
}

## VMs
data "yandex_compute_image" "container-optimized-image" {
  family = "container-optimized-image"
}

resource "yandex_compute_instance" "products-vm" {
  name               = "products-vm"
  platform_id        = "standard-v3"
  service_account_id = var.service_account_id

  boot_disk {
    initialize_params {
      image_id = data.yandex_compute_image.container-optimized-image.id
    }
  }

  network_interface {
    subnet_id = yandex_vpc_subnet.subnet1.id
    nat       = !var.production_mode
  }

  resources {
    cores         = 2
    memory        = 1
    core_fraction = 20
  }

  scheduling_policy {
    preemptible = true
  }

  metadata = {
    docker-container-declaration = templatefile("${path.module}/coi_specs/products_spec.yml", {
      pg_host     = yandex_mdb_postgresql_cluster.zenithar-pgsql.host.0.fqdn
      pg_db       = var.pg_db
      pg_username = var.pg_username
      pg_password = var.pg_password
    })

    ssh-keys = "${var.vm_username}:${var.public_ssh}"
  }

  depends_on = [
    yandex_mdb_postgresql_cluster.zenithar-pgsql
  ]
}

output "products_url" {
  value = "http://${yandex_compute_instance.products-vm.network_interface.0.nat_ip_address}"
}

output "products_ssh" {
  value = "ssh ${var.vm_username}@${yandex_compute_instance.products-vm.network_interface.0.nat_ip_address}"
}

resource "yandex_lb_target_group" "products_lb_tg" {
  name      = "products-tg"
  region_id = "ru-central1"

  target {
    subnet_id = yandex_vpc_subnet.subnet1.id
    address   = yandex_compute_instance.products-vm.network_interface.0.ip_address
  }

  depends_on = [
    yandex_compute_instance.products-vm
  ]
}

resource "yandex_lb_network_load_balancer" "products_lb" {
  name = "products-lb"

  listener {
    name = "default-listener"
    port = 80
    external_address_spec {
      ip_version = "ipv4"
    }
  }

  attached_target_group {
    target_group_id = yandex_lb_target_group.products_lb_tg.id

    healthcheck {
      name = "http"
      http_options {
        port = 80
        path = "/swagger/index.html"
      }
    }
  }

  depends_on = [
    yandex_lb_target_group.products_lb_tg
  ]
}

locals {
  products_lb_external_address_spec = one(yandex_lb_network_load_balancer.products_lb.listener).external_address_spec
  products_lb_url                   = "http://${one(local.products_lb_external_address_spec).address}"
}

output "products_lb_url" {
  value = local.products_lb_url
}

## Containers
resource "yandex_serverless_container" "web_container" {
  service_account_id = var.service_account_id

  name          = "zenithar-web"
  memory        = 256
  cores         = 1
  core_fraction = 20
  concurrency   = 1

  image {
    url = "cr.yandex/${var.yandex_container_registry_id}/zenithar-web"
    environment = {
      "ASPNETCORE_URLS" : "http://+:8080"
      "ProductsClient__ApiUri" : local.products_lb_url
      "AWS__AccessKeyId" : yandex_iam_service_account_static_access_key.sa-static-key.access_key
      "AWS__AccessKeySecret" : yandex_iam_service_account_static_access_key.sa-static-key.secret_key
      "AWS__BucketName" : var.bucket_name
    }
  }

  depends_on = [
    yandex_lb_network_load_balancer.products_lb,
    yandex_storage_bucket.bucket
  ]
}

output "web_container_url" {
  value = yandex_serverless_container.web_container.url
}

resource "yandex_api_gateway" "gateway" {
  name = "gateway"

  spec = templatefile("${path.module}/gateway_specs/gateway_spec.yml", {
    web_container_id   = yandex_serverless_container.web_container.id
    service_account_id = var.service_account_id
  })

  depends_on = [
    yandex_serverless_container.web_container
  ]
}

output "gateway_url" {
  value = "http://${yandex_api_gateway.gateway.domain}"
}

output "_info" {
  value = {
    "Zenithar avaliable here" : "http://${yandex_api_gateway.gateway.domain}"
    "Products api swagger" : "${local.products_lb_url}/swagger"
  }
}
