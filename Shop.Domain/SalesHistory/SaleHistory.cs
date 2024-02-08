using SharedKernel;
using Shop.Domain.Products;

namespace Shop.Domain.SalesHistory;

public class SaleHistory : AuditableEntity, IAggregateRoot
{
    public DateTime Date { get; private set; } = DateTime.UtcNow;
    public string ClientName { get; private set; }
    public int Quantity { get; private set; }
    public decimal PricePerUnit { get; private set; }
    public decimal TotalPrice { get { return CalculateTotalPriceBehaviour(Quantity, PricePerUnit); }
        private set {}
    }
    public virtual Product Product {get; private set; }
    public Guid ProductId { get; private set; }

    public static SaleHistory Create(string clientName, int quantity, decimal pricePerUnit, Guid productId)
    {
        SaleHistory saleHistory = new()
        {
            ClientName = clientName,
            Quantity = quantity,
            PricePerUnit = pricePerUnit
        };

        return saleHistory;
    }

    private decimal CalculateTotalPriceBehaviour(int quantity, decimal pricePerUnit)
     => pricePerUnit * quantity;
}