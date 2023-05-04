namespace Shop.Domain.Entities;

public class Product : BaseAuditiableEntity
{
    public string Description {get; set;}
    public string Name {get; set;}
    public decimal Price {get; set;}
    public int Quantity {get; set;}
    public bool IsAvaliable {get; set;}
}
