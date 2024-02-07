using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Mapping;
using Shop.Application.Common.Models;
using Shop.Application.Sale.DTOs;
using Shop.Application.Sale.Interfaces;
using Shop.Domain.Events;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.Sale.Commands;

public sealed record UpdateSaleCommand : IRequest<ApiResult<SaleDTO>>
{
    public Guid Id { get; init; }
    public DateTime SaleDate { get; init; }
    public Guid ClientId { get; init; }
    public int Quantity { get; init; }
    public decimal PricePerUnit { get; init; }
    public decimal TotalPrice { get; init; }
    public long ProductId { get; init; }
}

public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, ApiResult<SaleDTO>>
{
    private readonly IAppDbContext _context;
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    public UpdateSaleCommandHandler(IAppDbContext context, ISaleRepository saleRepository, IMapper mapper)
        => (_context, _saleRepository, _mapper) = (context, saleRepository, mapper);
    
    public async Task<ApiResult<SaleDTO>> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _saleRepository.FindByIdAsync(request.Id);

        if(entity is null)
            return new ApiResult<SaleDTO>(new SaleDTO(), ResponseTypeEnum.Warning, "Failed to update the register.");

        Entities.SalesHistory updateEntity = new()
        {
            Id = entity.Id,
            Date = entity.Date,	
            ClientId = entity.ClientId,	
            ProductId = entity.ProductId,	
            TotalPrice = entity.TotalPrice,	
            Quantity = entity.Quantity,
            PricePerUnit = entity.PricePerUnit,
        };
        
        updateEntity.Raise(new SaleCreateEvent(updateEntity));
        _saleRepository.SetEntityStateModified(updateEntity);
        await _context.SaveChangesAsync(cancellationToken);

        SaleDTO dto = _mapper.Map<SaleDTO>(updateEntity);
        return new ApiResult<SaleDTO>(dto, ResponseTypeEnum.Success, "Operation completed successfully.");
    }
} 