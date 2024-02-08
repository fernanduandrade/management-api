// using AutoMapper;
// using Microsoft.EntityFrameworkCore;
// using Shop.Application.Common.Interfaces;
// using Shop.Application.Common.Mapping;
// using Shop.Application.Common.Models;
// using Shop.Application.Product.Commands;
// using Shop.Application.Product.Interfaces;
// using Entities = Shop.Domain.Entities;
//
// namespace Shop.Application.UnitTest.Products.Commands;
//
// public class CreateProductCommnadHandlerTests
// {
//     private readonly Mock<IAppDbContext> _appContext;
//     private readonly Mock<IProductRepository> _productRepository;
//     private readonly IMapper _mapper;
//
//     public CreateProductCommnadHandlerTests()
//     {
//         _appContext = new();
//         _productRepository = new();
//         var mapperConfig = new MapperConfiguration(c =>
//         {
//             c.AddProfile<MappingProfile>();
//         });
//
//         _mapper = mapperConfig.CreateMapper();
//     }
//     
//     [Fact]
//     public async Task Handle_Should_Return_Type_Error_When_Id_Is_Zero()
//     {
//         // Arrange
//         _productRepository.Setup(x => x.IsProductUniqueAsync(
//             It.IsAny<string>()
//             )).ReturnsAsync(false);
//         var command = new CreateProductCommand()
//         {
//             Description = "Sunscreen Factor 50",
//             IsAvaliable = true,
//             Quantity = 10,
//             Name = "Sunscreen Factor",
//             Price = 21
//         };
//         
//         var mockProductsSet = new Mock<DbSet<Entities.Product>>();
//         mockProductsSet.Setup(m => m.Add(It.IsAny<Entities.Product>())).Callback<Entities.Product>((product) =>
//         {
//             product.Id = new Guid();
//             product.Description = command.Description;
//             product.Quantity = command.Quantity;
//             product.Name = command.Name;
//             product.Price = command.Price;
//         });
//
//         _appContext
//             .Setup(x => x.Products).Returns(mockProductsSet.Object);
//
//         var handler = new CreateProductCommandHandler(
//             _appContext.Object,
//             _productRepository.Object,
//             _mapper
//             );
//         
//         // Act
//         var result = await handler.Handle(command, default);
//         
//         // Assert
//         Assert.Equal(ResponseTypeEnum.Error, result.Type);
//     }
//     
//     [Fact]
//     public async Task Handle_Should_Return_Type_Success()
//     {
//         // Arrange
//         _productRepository.Setup(x => x.IsProductUniqueAsync(
//             It.IsAny<string>()
//         )).ReturnsAsync(false);
//         var command = new CreateProductCommand()
//         {
//             Description = "Sunscreen Factor 50",
//             IsAvaliable = true,
//             Quantity = 10,
//             Name = "Sunscreen Factor",
//             Price = 21,
//         };
//         
//         var mockProductsSet = new Mock<DbSet<Entities.Product>>();
//         mockProductsSet.Setup(m => m.Add(It.IsAny<Entities.Product>())).Callback<Entities.Product>((product) =>
//         {
//             product.Id = new Guid();
//             product.Description = command.Description;
//             product.Quantity = command.Quantity;
//             product.Name = command.Name;
//             product.Price = command.Price;
//         });
//
//         _appContext
//             .Setup(x => x.Products).Returns(mockProductsSet.Object);
//
//         var handler = new CreateProductCommandHandler(
//             _appContext.Object,
//             _productRepository.Object,
//             _mapper
//         );
//         
//         // Act
//         var result = await handler.Handle(command, default);
//         
//         // Assert
//         Assert.Equal(ResponseTypeEnum.Success, result.Type);
//     }
//
//     [Fact]
//     public async Task Handle_Should_Return_Type_Warning_If_Product_Exists()
//     {
//         // Arrange
//         _productRepository.Setup(x => x.IsProductUniqueAsync(
//             It.IsAny<string>()
//         )).ReturnsAsync(true);
//         var command = new CreateProductCommand()
//         {
//             Description = "Ice Cream",
//             IsAvaliable = true,
//             Quantity = 4,
//             Name = "Ice Cream",
//             Price = 10,
//         };
//         var mockProductsSet = new Mock<DbSet<Entities.Product>>();
//
//         _appContext
//             .Setup(x => x.Products).Returns(mockProductsSet.Object);
//
//         var handler = new CreateProductCommandHandler(
//             _appContext.Object,
//             _productRepository.Object,
//             _mapper
//         );
//         
//         // Act
//         var result = await handler.Handle(command, default);
//         
//         // Assert
//         Assert.Equal(ResponseTypeEnum.Warning, result.Type);
//     }
// }