// using Microsoft.EntityFrameworkCore;
// using Manager.Application.Common.Interfaces;
// using Manager.Application.Common.Models;
// using Manager.Application.Product.Commands;
// using Manager.Application.Product.Interfaces;
// using Entities = Manager.Domain.Entities;
//
// namespace Manager.Application.UnitTest.Products.Commands;
//
// public class DeleteProductCommandHandlerTest
// {
//     private readonly Mock<IAppDbContext> _appContext;
//     private readonly Mock<IProductRepository> _productRepository;
//
//     public DeleteProductCommandHandlerTest()
//     {
//         _appContext = new();
//         _productRepository = new();
//     }
//
//     [Fact]
//     public async Task Product_Does_Not_Exists_Should_Return_Type_Error()
//     {
//         // Arrange
//         Entities.Product product = null;
//         _productRepository.Setup(x => x.FindByIdAsync(
//             It.IsAny<Guid>()
//         )).ReturnsAsync(product);
//         
//         var command = new DeleteProductCommand()
//         {
//             Id = new Guid()
//         };
//         var mockProductsSet = new Mock<DbSet<Entities.Product>>();
//         mockProductsSet.Setup(m => m.Add(It.IsAny<Entities.Product>())).Callback<Entities.Product>((product) =>
//         {
//             product.Id = new Guid();
//             product.Description = "Cheese";
//             product.Quantity = 3;
//             product.Name = "Cheese";
//             product.Price = 30;
//         });
//
//         _appContext
//             .Setup(x => x.Products).Returns(mockProductsSet.Object);
//         var handler = new DeleteProductCommandHandler(
//             _appContext.Object,
//             _productRepository.Object
//         );
//         
//         // Act
//         var result = await handler.Handle(command, default);
//         // Assert
//
//         Assert.Equal(ResponseTypeEnum.Error, result.Type);
//     }
//     
//     [Fact]
//     public async Task Product_Not_Exists_Should_Return_Type_Error()
//     {
//         // Arrange
//         Entities.Product product = new()
//         {
//             Id = new Guid(),
//             Description = "Cheese",
//             Quantity = 3,
//             Name = "Cheese",
//             Price = 30,
//         };
//         _productRepository.Setup(x => x.FindByIdAsync(
//             It.IsAny<Guid>()
//         )).ReturnsAsync(product);
//         
//         var command = new DeleteProductCommand()
//         {
//             Id = new Guid()
//         };
//         var mockProductsSet = new Mock<DbSet<Entities.Product>>();
//         mockProductsSet.Setup(m => m.Add(It.IsAny<Entities.Product>())).Callback<Entities.Product>((product) =>
//         {
//             product.Id = new Guid();
//             product.Description = "Cheese";
//             product.Quantity = 3;
//             product.Name = "Cheese";
//             product.Price = 30;
//         });
//
//         _appContext
//             .Setup(x => x.Products).Returns(mockProductsSet.Object);
//         var handler = new DeleteProductCommandHandler(
//             _appContext.Object,
//             _productRepository.Object
//         );
//         
//         // Act
//         var result = await handler.Handle(command, default);
//         // Assert
//
//         Assert.Equal(ResponseTypeEnum.Success, result.Type);
//     }
// }