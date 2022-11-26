namespace Zenithar.BFF.Core;

public sealed class Product : EntityBase<string>
{
    public string Name { get; }
    public double Price { get; }
    public string PreviewUrl { get; }

    public Product(string id, string name, double price, string previewUrl) : base(id)
    {
        Name = name;
        Price = price;
        PreviewUrl = previewUrl;
    }
}