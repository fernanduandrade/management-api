using SharedKernel;

namespace Shop.Domain.Products;

public class Product : AuditableEntity, IAggregateRoot
{
    public string? Description {get; private set;}
    public string? Name {get; private set;}
    public decimal Price {get; private set;}
    public int Quantity {get; private set;}

    public bool IsAvaliable => IsAvaliableBehavior(Quantity);

    private bool IsAvaliableBehavior(int quantity)
        => quantity > 0;

    public static Product Create(string description, string name, decimal price, int quantity)
    {
        Product product = new()
        {
            Id = new Guid(),
            Description = description,
            Name = name,
            Price = price,
            Quantity = quantity
        };
        return product;
    }

    public void SetQuantity(int quantity)
    {
        Quantity += quantity;
    }
}