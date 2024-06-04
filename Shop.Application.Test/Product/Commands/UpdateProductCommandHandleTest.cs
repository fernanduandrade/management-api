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
// public class UpdateProductCommandHandleTest
// {
//     private readonly Mock<IAppDbContext> _appDbContext;
//     private readonly Mock<IProductRepository> _productRepository;
//     private readonly IMapper _mapper;
//     public UpdateProductCommandHandleTest()
//     {
//         _appDbContext = new();
//         _productRepository = new();
//         
//         var mapperConfig = new MapperConfiguration(c =>
//         {
//             c.AddProfile<MappingProfile>();
//         });
//
//         _mapper = mapperConfig.CreateMapper();
//     }
//
//     [Fact]
//     public async Task Update_Product_Doesnt_Exists_Should_Return_Warning()
//     {
//         Entities.Product product = null;
//
//         _productRepository.Setup(x => x.FindByIdAsync(
//             It.IsAny<Guid>()
//         )).ReturnsAsync(product);
//
//         var command = new UpdateProductCommand()
//         {
//             Id = new Guid(),
//             Description = "Milk",
//             Quantity = 10,
//             Price = 20,
//             Name = "Milk"
//         };
//         
//         var mockProductsSet = new Mock<DbSet<Entities.Product>>();
//         mockProductsSet.Setup(m => m.Add(It.IsAny<Entities.Product>()));
//
//         _appDbContext
//             .Setup(x => x.Products).Returns(mockProductsSet.Object);
//         var handler = new UpdateProductCommandHandler(
//             _appDbContext.Object,
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
//     
//     [Fact]
//     public async Task Update_Product_Should_Return_Success()
//     {
//         Entities.Product product = new()
//         {
//             Id = new Guid(),
//             Description = "Milk",
//             Quantity = 10,
//             Price = 20,
//             Name = "Milk"
//         };
//         
//         _productRepository.Setup(x => x.FindByIdAsync(
//             It.IsAny<Guid>()
//         )).ReturnsAsync(product);
//         
//         var command = new UpdateProductCommand()
//         {
//             Id = new Guid(),
//             Description = "Milk Cheap",
//             Quantity = 2,
//             Price = 20,
//             Name = "Milk"
//         };
//         
//         var mockProductsSet = new Mock<DbSet<Entities.Product>>();
//         mockProductsSet.Setup(m => m.Add(It.IsAny<Entities.Product>()));
//
//         _appDbContext
//             .Setup(x => x.Products).Returns(mockProductsSet.Object);
//         var handler = new UpdateProductCommandHandler(
//             _appDbContext.Object,
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
//     public async Task Update_Product_When_Quantity_Is_Zero_Return_Product_Not_Avaliable()
//     {
//         Entities.Product product = new()
//         {
//             Id = new Guid(),
//             Description = "Milk",
//             Quantity = 10,
//             Price = 20,
//             Name = "Milk"
//         };
//         
//         _productRepository.Setup(x => x.FindByIdAsync(
//             It.IsAny<Guid>()
//         )).ReturnsAsync(product);
//         
//         var command = new UpdateProductCommand()
//         {
//             Id = new Guid(),
//             Description = "Milk Cheap",
//             Quantity = 0,
//             Price = 20,
//             Name = "Milk"
//         };
//         
//         var mockProductsSet = new Mock<DbSet<Entities.Product>>();
//         mockProductsSet.Setup(m => m.Add(It.IsAny<Entities.Product>()));
//
//         _appDbContext
//             .Setup(x => x.Products).Returns(mockProductsSet.Object);
//         var handler = new UpdateProductCommandHandler(
//             _appDbContext.Object,
//             _productRepository.Object,
//             _mapper
//         );
//         
//         // Act
//         var result = await handler.Handle(command, default);
//
//         bool expected = false;
//         // Assert
//         Assert.Equal(expected, result.Data.IsAvaliable);
//     }
//     
//     [Fact]
//     public async Task Update_Product_When_Quantity_Is_Greater_Than_Zero_Return_Product_Avaliable()
//     {
//         Entities.Product product = new()
//         {
//             Id = new Guid(),
//             Description = "Milk",
//             Quantity = 10,
//             Price = 20,
//             Name = "Milk"
//         };
//         
//         _productRepository.Setup(x => x.FindByIdAsync(
//             It.IsAny<Guid>()
//         )).ReturnsAsync(product);
//         
//         var command = new UpdateProductCommand()
//         {
//             Id = new Guid(),
//             Description = "Milk Cheap",
//             Quantity = 1,
//             Price = 20,
//             Name = "Milk"
//         };
//         
//         var mockProductsSet = new Mock<DbSet<Entities.Product>>();
//         mockProductsSet.Setup(m => m.Add(It.IsAny<Entities.Product>()));
//
//         _appDbContext
//             .Setup(x => x.Products).Returns(mockProductsSet.Object);
//         var handler = new UpdateProductCommandHandler(
//             _appDbContext.Object,
//             _productRepository.Object,
//             _mapper
//         );
//         
//         // Act
//         var result = await handler.Handle(command, default);
//
//         bool expected = true;
//         // Assert
//         Assert.Equal(expected, result.Data.IsAvaliable);
//     }
//     
//     [Fact]
//     public async Task Update_Product_Properties()
//     {
//         Entities.Product product = new()
//         {
//             Id = new Guid(),
//             Description = "Milk",
//             Quantity = 10,
//             Price = 20,
//             Name = "Milk"
//         };
//         
//         _productRepository.Setup(x => x.FindByIdAsync(
//             It.IsAny<Guid>()
//         )).ReturnsAsync(product);
//         
//         var command = new UpdateProductCommand()
//         {
//             Id = new Guid(),
//             Description = "Milk Cheap Gold",
//             Quantity = 1,
//             Price = 10,
//             Name = "Milk Gold"
//         };
//         
//         var mockProductsSet = new Mock<DbSet<Entities.Product>>();
//         mockProductsSet.Setup(m => m.Add(It.IsAny<Entities.Product>()));
//
//         _appDbContext
//             .Setup(x => x.Products).Returns(mockProductsSet.Object);
//         var handler = new UpdateProductCommandHandler(
//             _appDbContext.Object,
//             _productRepository.Object,
//             _mapper
//         );
//         
//         // Act
//         var result = await handler.Handle(command, default);
//
//         string expectedDescription = "Milk Cheap Gold";
//         string expectedName = "Milk Gold";
//         decimal expectedPrice = 10;
//         decimal expectedQuantity = 1;
//         // Assert
//         Assert.Equal(expectedDescription, result.Data.Description);
//         Assert.Equal(expectedName, result.Data.Name);
//         Assert.Equal(expectedPrice, result.Data.Price);
//         Assert.Equal(expectedQuantity, result.Data.Quantity);
//     }
// }