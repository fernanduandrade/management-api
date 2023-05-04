using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Entities = Shop.Domain.Entities;
using Shop.Domain.Events;

namespace Shop.Application.Product.Commands;

public record UpdateProductCommand : IRequest<ApiResult<bool>>
{
    public int Id {get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResult<bool>>
{
    private readonly IAppDbContext _context;

    public UpdateProductCommandHandler(IAppDbContext context)
     => (_context) = (context);

    public async Task<ApiResult<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context
            .Products
            .AsNoTracking()
            .FirstOrDefaultAsync(product => product.Id == request.Id);

        if(entity is null)
            return new ApiResult<bool>(false, "Falha ao executar a operação");
    
        Entities.Product updateEntity = new()
        {
            Description = request.Description,
            Id = request.Id,
            IsAvaliable = request.Quantity <= 0 ? false : true,
            Name = request.Name,
            Price = request.Price,
            Quantity = request.Quantity,
        };

        updateEntity.AddDomainEvent(new ProductCreateEvent(updateEntity));
        _context.Products.Entry(updateEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return new ApiResult<bool>(true, "Operação concluida com sucesso");
    }
}
