namespace Zenithar.BFF.Core;

public sealed record ProductBatch(Product Product, int Quantity)
{
    public int Quantity { get; set; } = Quantity;

    public double TotalPrice => Quantity * Product.Price;
}