using AutoMapper;
using Shop.Application.Common.Mapping;
using Shop.Application.Common.Models;
using Shop.Application.Sale.Interfaces;
using Shop.Application.Sale.Queries;
using Shop.Application.UnitTest.Mocks;
using Shouldly;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.UnitTest.Sale.Queries;

public class GetSaleByIdHandlerTest
{
    private readonly Mock<ISaleRepository> _saleRepository;
    private readonly IMapper _mapper;
    public GetSaleByIdHandlerTest()
    {
        _saleRepository = MockSaleRepository.GetSaleRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Sale_Doesnt_Exists_Should_Return_Data_Null()
    {
        var query = new GetSaleByIdQuery() { Id = 19 };
        Entities.Sale saleDtoExpected = null;
        _saleRepository
            .Setup(x => x.FindByIdAsync(query.Id))
            .ReturnsAsync(saleDtoExpected);

        var handler = new GetSaleByQueryHandler(
            _saleRepository.Object,
            _mapper
        );

        var result = await handler.Handle(query, CancellationToken.None);
        
        result.Data.ShouldBeNull();
    }
    
    [Fact]
    public async Task Client_Exists_Should_Return_Type_Successful()
    {
        var query = new GetSaleByIdQuery() { Id = 31 };
        Entities.Product product = new() { Id = 2, Name = "Charger", Description = "", Created = DateTime.Now, Price = 12, Quantity = 5 };
        Entities.Sale saletDtoExpected = new() { Id = 5, Product = product, ProductFk = 2, Quantity = 2, ClientName = "Fernando", SaleDate = DateTime.Now, TotalPrice = 24 };
        string messageExpected = "Operation completed successfully.";
        _saleRepository
            .Setup(x => x.FindByIdAsync(query.Id))
            .ReturnsAsync(saletDtoExpected);

        var handler = new GetSaleByQueryHandler(
            _saleRepository.Object,
            _mapper
        );

        var result = await handler.Handle(query, CancellationToken.None);
        
        result.Message.ShouldBe(messageExpected);
        result.Type.ShouldBe(ResponseTypeEnum.Success);
    }
}