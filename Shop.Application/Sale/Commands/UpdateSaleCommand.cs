using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Mapping;
using Shop.Application.Common.Models;
using Shop.Application.Sale.DTOs;
using Shop.Domain.Events;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.Sale.Commands;

public sealed record UpdateSaleCommand : IRequest<ApiResult<SaleDTO>>, IMapFrom<Entities.Sale>
{
    public long Id { get; init; }
    public DateTime SaleDate { get; init; }
    public string ClientName { get; init; }
    public string ProductName { get; init; }
    public int Quantity { get; init; }
    public decimal PricePerUnit { get; init; }
    public decimal TotalPrice { get; init; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Entities.Sale, UpdateSaleCommand>();
    }
}

public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, ApiResult<SaleDTO>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateSaleCommandHandler(IAppDbContext context, IMapper mapper)
        => (_context, _mapper) = (context, mapper);
    
    public async Task<ApiResult<SaleDTO>> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context
            .Sales
            .AsNoTracking()
            .FirstOrDefaultAsync(sale => sale.Id == request.Id);

        if(entity is null)
            return new ApiResult<SaleDTO>(_mapper.Map<SaleDTO>(request), ResponseTypeEnum.Warning, "Failed to update the register.");

        var updateEntity = _mapper.Map<Entities.Sale>(request);
        
        updateEntity.AddDomainEvent(new SaleCreateEvent(updateEntity));
        _context.Sales.Entry(updateEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);

        SaleDTO dto = _mapper.Map<SaleDTO>(updateEntity);
        
        return new ApiResult<SaleDTO>(dto, ResponseTypeEnum.Success, "Operation completed successfully.");
    }
} 