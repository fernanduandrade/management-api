using SharedKernel;

namespace Shop.Domain.Clients;

public class Client : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public bool IsActive { get; private set; }
    public string Phone { get; private set; }
    public decimal Debt { get; private set; }
    public decimal Credit { get; private set; }


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


    public void SetDebtInput(decimal value)
    {
        Debt += value;
    }

    public void SetCreditInput(decimal value)
    {
        Credit += value;
    }

    public void ChangeStatus(){
        IsActive = !IsActive;
    }
}