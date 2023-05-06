using AutoMapper;
using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Domain.Events;

namespace Shop.Application.Sale.Commands;

public sealed record CreateSaleCommand : IRequest<ApiResult<bool>>
{
    public DateTime SaleDate { get; init; }
    public string ClientName { get; init; }
    public string ProductName { get; init; }
    public int Quantity { get; init; }
    public decimal PricePerUnit { get; init; }
    public decimal TotalPrice { get; init; }
}

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, ApiResult<bool>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateSaleCommandHandler(IAppDbContext context, IMapper mapper)
        => (_context, _mapper) = (context, mapper);
    public async Task<ApiResult<bool>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Sale entity = new()
        {
            ClientName = request.ClientName,
            ProductName = request.ProductName,
            TotalPrice = request.TotalPrice,
            PricePerUnit = request.PricePerUnit,
            SaleDate = request.SaleDate,
            Quantity = request.Quantity
        };
        
        entity.AddDomainEvent(new SaleCreateEvent(entity));
        _context.Sales.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResult<bool>(true);
    }
}