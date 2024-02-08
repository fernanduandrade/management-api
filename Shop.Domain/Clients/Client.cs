using SharedKernel;

namespace Shop.Domain.Clients;

public class Client : AuditableEntity, IAggregateRoot
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }
    public string Phone { get; set; }
    public decimal Debt { get; set; }
    public decimal Credit { get; set; }


    public static Client Create(string name, string lastName, string phone, bool isActive, decimal debt, decimal credit)
    {
        Client client = new()
        {
            Id = new Guid(),
            Name = name,
            LastName = lastName,
            Debt = debt,
            Credit = credit,
            Phone = phone,
            IsActive = isActive
        };

        return client;
    }
}