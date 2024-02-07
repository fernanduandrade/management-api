using SharedKernel;

namespace Shop.Domain.Entities;

public class Product : AuditableEntity
{
    public string Description {get; set;}
    public string Name {get; set;}
    public decimal Price {get; set;}
    public int Quantity {get; set;}
    public bool IsAvaliable
    {
        get { return IsAvaliableBehavior(Quantity); }
        private set {}
    } 

    private bool IsAvaliableBehavior(int quantity)
        => quantity > 0;
}
