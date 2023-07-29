using Moq;
using Shop.Application.Client.Interfaces;

namespace Shop.Application.UnitTest.Mocks;

public class MockClientRepository
{
    public static Mock<IClientRepository> GetClientRepository()
    {
        Mock <IClientRepository> mockRepository = new();
        
        return mockRepository;
    }
}