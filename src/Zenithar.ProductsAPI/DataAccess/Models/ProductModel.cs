﻿namespace Zenithar.ProductsAPI.DataAccess.Models;

internal sealed class ProductModel
{
    public string Id { get; private set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string PreviewUrl { get; set; }

    private ProductModel() { }

    public ProductModel(string id, string name, double price, string previewUrl)
    {
        Id = id;
        Name = name;
        Price = price;
        PreviewUrl = previewUrl;
    }
}