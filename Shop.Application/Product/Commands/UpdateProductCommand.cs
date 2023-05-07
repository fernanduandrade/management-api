using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Product.DTOs;
using Entities = Shop.Domain.Entities;
using Shop.Domain.Events;

namespace Shop.Application.Product.Commands;

public sealed record UpdateProductCommand : IRequest<ApiResult<ProductDTO>>
{
    public int Id {get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
    public bool IsAvaliable { get; init; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResult<ProductDTO>>
{
    private readonly IAppDbContext _context;

    public UpdateProductCommandHandler(IAppDbContext context)
        => (_context) = (context);

    public async Task<ApiResult<ProductDTO>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context
            .Products
            .AsNoTracking()
            .FirstOrDefaultAsync(product => product.Id == request.Id);

        if(entity is null)
            return new ApiResult<ProductDTO>(new ProductDTO(), ResponseTypeEnum.Warning, "Failed to update the register.");

        Entities.Product updateEntity = new()
        {	
            Description = request.Description,	
            Id = request.Id,	
            IsAvaliable = request.IsAvaliable,	
            Name = request.Name,	
            Price = request.Price,	
            Quantity = request.Quantity,	
        };

        updateEntity.AddDomainEvent(new ProductCreateEvent(updateEntity));
        _context.Products.Entry(updateEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);

        ProductDTO dto = new()
        {
            Description = updateEntity.Description,
            Id = updateEntity.Id,
            IsAvaliable = updateEntity.IsAvaliable,
            Name = updateEntity.Name,
            Price = updateEntity.Price,
            Quantity = updateEntity.Quantity,
        };

        return new ApiResult<ProductDTO>(dto, ResponseTypeEnum.Success ,"Operation completed successfully.");
    }
}
