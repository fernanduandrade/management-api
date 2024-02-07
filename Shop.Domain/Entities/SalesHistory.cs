using SharedKernel;

namespace Shop.Domain.Entities;

public class SalesHistory : AuditableEntity
{
    public DateTime Date { get; set; }
    public virtual Client Client { get; set; }
    public Guid ClientId { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerUnit { get; set; }
    public decimal TotalPrice { get; set; }
    public virtual Product Product {get; set; }
    public Guid ProductId { get; set; }
}