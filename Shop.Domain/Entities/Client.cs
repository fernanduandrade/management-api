namespace Shop.Domain.Entities;

public class Client
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }
    public string Phone { get; set; }
    public decimal Debt { get; set; }
    public decimal Credit { get; set; }
}
