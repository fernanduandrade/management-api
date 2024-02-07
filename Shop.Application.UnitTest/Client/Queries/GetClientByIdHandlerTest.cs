using AutoMapper;
using Shop.Application.Client.Interfaces;
using Shop.Application.Client.Queries;
using Shop.Application.Common.Mapping;
using Shop.Application.Common.Models;
using Shop.Application.UnitTest.Mocks;
using Shouldly;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.UnitTest.Client.Queries;

public class GetClientByIdHandlerTest
{
    private readonly Mock<IClientRepository> _clientRepository;
    private readonly IMapper _mapper;
    public GetClientByIdHandlerTest()
    {
        _clientRepository = MockClientRepository.GetClientRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Client_Doesnt_Exists_Should_Return_Data_Null()
    {
        var query = new GetClientByIdQuery() { Id = new Guid() };
        Entities.Client clientDtoExpected = null;
        _clientRepository
            .Setup(x => x.FindByIdAsync(query.Id))
            .ReturnsAsync(clientDtoExpected);

        var handler = new GetClientByIdQueryHandler(
            _clientRepository.Object,
            _mapper
        );

        var result = await handler.Handle(query, CancellationToken.None);
        
        result.Data.ShouldBeNull();
    }
    
    [Fact]
    public async Task Client_Exists_Should_Return_Type_Successful()
    {
        var query = new GetClientByIdQuery() { Id = new Guid() };
        Entities.Client clientDtoExpected = new() { Id = new Guid(), Name = "Clara", LastName = "Siqueira", Credit = 120, Debt = 0, Phone = "00000000000", IsActive = true };
        string messageExpected = "Operation completed successfully.";
        _clientRepository
            .Setup(x => x.FindByIdAsync(query.Id))
            .ReturnsAsync(clientDtoExpected);

        var handler = new GetClientByIdQueryHandler(
            _clientRepository.Object,
            _mapper
        );

        var result = await handler.Handle(query, CancellationToken.None);
        
        result.Message.ShouldBe(messageExpected);
        result.Type.ShouldBe(ResponseTypeEnum.Success);
    }
}