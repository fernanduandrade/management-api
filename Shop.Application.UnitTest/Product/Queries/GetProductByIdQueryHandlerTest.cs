using AutoMapper;
using Moq;
using Shop.Application.Common.Mapping;
using Shop.Application.Common.Models;
using Shop.Application.Product.DTOs;
using Shop.Application.Product.Interfaces;
using Shop.Application.Product.Queries;
using Shop.Application.UnitTest.Mocks;
using Shouldly;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.UnitTest.Products.Queries;

public class GetProductByIdQueryHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IProductRepository> _productRepository;

    public GetProductByIdQueryHandlerTest()
    {
        _productRepository = MockProductRepository.GetProductRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }
    
    [Fact]
    public async Task Get_By_Id_Should_Return_ProductDTOType()
    {
        var handler = new GetProductByIdQueryHandler(
            _productRepository.Object,
            _mapper
        );

        var result = await handler.Handle(new GetProductByIdQuery() { Id = new Guid() }, CancellationToken.None);
        
        result.ShouldBeOfType<ApiResult<ProductDTO>>();
    }
    
    [Fact]
    public async Task Get_Product_Doesnt_Exists_ShouldReturn_Data_Null()
    {

        Entities.Product expected = null;
        _productRepository
            .Setup(x => x.FindByIdAsync(new Guid()))
            .ReturnsAsync(expected);
        
        var handler = new GetProductByIdQueryHandler(
            _productRepository.Object,
            _mapper
        );

        var result = await handler.Handle(new GetProductByIdQuery() { Id = new Guid() }, CancellationToken.None);
        
        result.Data.ShouldBeNull();
    }
    
    [Fact]
    public async Task Get_Product_By_Id_Exits_ShouldReturn_Data()
    {

        Entities.Product entity = new() { Id = new Guid(), Name = "Tomato", Description = "Tomato", Price = 10, Quantity = 43 };

        var query = new GetProductByIdQuery() { Id = new Guid() };
        
        _productRepository
            .Setup(x => x.FindByIdAsync(query.Id))
            .ReturnsAsync(entity);
        
        var handler = new GetProductByIdQueryHandler(
            _productRepository.Object,
            _mapper
        );

        var result = await handler.Handle(query, CancellationToken.None);
        
        result.Data.ShouldNotBeNull();
        result.Data.Id.ShouldBe(19);
    }
    
    [Fact]
    public async Task Get_Product_By_Id_Doesnt_Exists_ShouldReturn_Type_Warning()
    {
        var query = new GetProductByIdQuery() { Id = new Guid() };

        var handler = new GetProductByIdQueryHandler(
            _productRepository.Object,
            _mapper
        );

        var result = await handler.Handle(query, CancellationToken.None);
        
        result.Type.ShouldBe(ResponseTypeEnum.Warning);
    }
}