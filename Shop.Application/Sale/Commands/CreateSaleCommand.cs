using AutoMapper;
using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Mapping;
using Shop.Application.Common.Models;
using Shop.Application.Sale.DTOs;
using Shop.Domain.Events;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.Sale.Commands;

public sealed record CreateSaleCommand : IRequest<ApiResult<SaleDTO>>, IMapFrom<Entities.Sale>
{
    public DateTime SaleDate { get; init; }
    public string ClientName { get; init; }
    public string ProductName { get; init; }
    public int Quantity { get; init; }
    public decimal PricePerUnit { get; init; }
    public decimal TotalPrice { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Entities.Sale, CreateSaleCommand>();
    }
}

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, ApiResult<SaleDTO>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateSaleCommandHandler(IAppDbContext context, IMapper mapper)
        => (_context, _mapper) = (context, mapper);
    public async Task<ApiResult<SaleDTO>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Entities.Sale>(request);
        
        entity.AddDomainEvent(new SaleCreateEvent(entity));
        _context.Sales.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        SaleDTO dto = _mapper.Map<SaleDTO>(entity);
        
        if (entity.Id <= 0)
            return new ApiResult<SaleDTO>(dto, ResponseTypeEnum.Error, "Error while trying to create the register.");
        
        return new ApiResult<SaleDTO>(dto, ResponseTypeEnum.Success, "Operation completed successfully.");
    }
}