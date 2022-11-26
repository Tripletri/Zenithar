namespace Zenithar.ProductsAPI.Core;

public sealed class Product : EntityBase<string>
{
    public string Name { get; }
    public string Price { get; }
    public string PreviewUrl { get; }

    public Product(string id, string name, string price, string previewUrl) : base(id)
    {
        Name = name;
        Price = price;
        PreviewUrl = previewUrl;
    }
}