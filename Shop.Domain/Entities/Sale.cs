namespace Shop.Domain.Entities;

public class Sale : BaseAuditiableEntity
{
    public DateTime SaleDate { get; set; }
    public string ClientName { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerUnit { get; set; }
    public decimal TotalPrice { get; set; }
    public Product Product {get; set; }
    public long ProductFk { get; set; }
}