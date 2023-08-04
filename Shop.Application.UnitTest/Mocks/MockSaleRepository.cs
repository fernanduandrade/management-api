using Shop.Application.Sale.Interfaces;

namespace Shop.Application.UnitTest.Mocks;

public class MockSaleRepository
{
    public static Mock<ISaleRepository> GetSaleRepository()
    {
        Mock <ISaleRepository> mockRepository = new();
        
        return mockRepository;
    }
}