using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Domain.Events;

namespace Shop.Application.Product.Commands;

public sealed record CreateProductCommand : IRequest<ApiResult<bool>>
{
    public string Name { get; init; }
    public string Description { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
    public bool IsAvaliable { get; init; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResult<bool>>
{
    private readonly IAppDbContext _context;

    public CreateProductCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    public async Task<ApiResult<bool>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Product entity = new()
        {
            Description = request.Description,
            Name = request.Name,
            IsAvaliable = false,
            Price = request.Price,
            Quantity = request.Quantity
        };
        
        entity.AddDomainEvent(new ProductCreateEvent(entity));
        _context.Products.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResult<bool>(true);
    }
}