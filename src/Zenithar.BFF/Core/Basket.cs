using Zenithar.BFF.Exceptions;

namespace Zenithar.BFF.Core;

public sealed class Basket : EntityBase<string>
{
    public IReadOnlyCollection<ProductBatch> ProductBatches => productBatches.AsReadOnly();
    public double TotalPrice => productBatches.Sum(x => x.TotalPrice);
    public int TotalCount => productBatches.Sum(x => x.Quantity);

    private readonly List<ProductBatch> productBatches = new();

    public Basket(string id) : base(id) { }

    public void AddProduct(Product product)
    {
        var existingBatch = productBatches.FirstOrDefault(x => x.Product.Id == product.Id);

        if (existingBatch != null)
        {
            existingBatch.Quantity++;
            return;
        }

        productBatches.Add(new ProductBatch(product, 1));
    }

    public void RemoveProduct(Product product)
    {
        var existingBatch = productBatches.FirstOrDefault(x => x.Product.Id == product.Id);

        if (existingBatch == null)
            throw new NotFoundException("Product not in basket");

        existingBatch.Quantity--;

        if (existingBatch.Quantity == 0)
            productBatches.Remove(existingBatch);
    }

    public void Clear()
    {
        productBatches.Clear();
    }
}