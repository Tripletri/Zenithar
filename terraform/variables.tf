variable "yandex_iam_token" {
  type = string
  sensitive = true
}

variable "cloud_id" {
  type = string
}

variable "folder_id" {
  type = string
}

variable "service_account_id" {
  type = string
}

variable "vm_username" {
  type    = string
  default = "adam"
}

variable "public_ssh" {
  type = string
}

variable "pg_db" {
  type    = string
  default = "zenithar"
}

variable "pg_username" {
  type    = string
  default = "pguser"
}

variable "pg_password" {
  type    = string
  default = "pgpassword"
  sensitive = true
}

variable "bucket_name" {
  type    = string
  default = "zenithar"
}

variable "yandex_container_registry_id" {
  type = string
}

variable "production_mode" {
  type    = bool
  default = true
  description = "Using to determine some variables. For example Postgres environment"
}
