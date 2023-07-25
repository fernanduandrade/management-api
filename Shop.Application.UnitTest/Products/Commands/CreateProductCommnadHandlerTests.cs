using Microsoft.EntityFrameworkCore;
using Moq;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Application.Product.Commands;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.UnitTest.Products.Commands;

public class CreateProductCommnadHandlerTests
{
    private readonly Mock<IAppDbContext> _appContext;

    public CreateProductCommnadHandlerTests()
    {
        _appContext = new();
    }
    [Fact]
    public async Task Handle_Should_Return_Type_Error_When_Id_Is_Zero()
    {
        // Arrange
        var command = new CreateProductCommand()
        {
            
        };
        
        var mockProductsSet = new Mock<DbSet<Entities.Product>>();
        mockProductsSet.Setup(m => m.Add(It.IsAny<Entities.Product>())).Callback<Entities.Product>((product) =>
        {
            product.Id = 0;
            product.Description = "Protetor Fator 50";
            product.IsAvaliable = true;
            product.Quantity = 10;
            product.Name = "Protetor Solar";
            product.Price = 21;
        });

        _appContext
            .Setup(x => x.Products).Returns(mockProductsSet.Object);

        var handler = new CreateProductCommandHandler(
            _appContext.Object
            );
        
        // Act
        var result = await handler.Handle(command, default);
        
        // Assert
        Assert.Equal(result.Type, ResponseTypeEnum.Error);
    }
    
    [Fact]
    public async Task Handle_Should_Return_Type_Success()
    {
        // Arrange
        var command = new CreateProductCommand()
        {
            Description = "Protetor Fator 50",
            IsAvaliable = true,
            Quantity = 10,
            Name = "Protetor Solar",
            Price = 21,
        };
        
        var mockProductsSet = new Mock<DbSet<Entities.Product>>();
        mockProductsSet.Setup(m => m.Add(It.IsAny<Entities.Product>())).Callback<Entities.Product>((product) =>
        {
            product.Id = 21;
            product.Description = command.Description;
            product.IsAvaliable = command.IsAvaliable;
            product.Quantity = command.Quantity;
            product.Name = command.Name;
            product.Price = command.Price;
        });

        _appContext
            .Setup(x => x.Products).Returns(mockProductsSet.Object);

        var handler = new CreateProductCommandHandler(
            _appContext.Object
        );
        
        // Act
        var result = await handler.Handle(command, default);
        
        // Assert
        Assert.Equal(result.Type, ResponseTypeEnum.Success);
    }
}