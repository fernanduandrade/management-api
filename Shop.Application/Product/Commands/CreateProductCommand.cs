using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Application.Product.DTOs;
using Shop.Application.Product.Interfaces;
using Shop.Domain.Events;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.Product.Commands;

public sealed record CreateProductCommand : IRequest<ApiResult<ProductDTO>>
{
    public string Name { get; init; }
    public string Description { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
    public bool IsAvaliable { get; init; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResult<ProductDTO>>
{
    private readonly IAppDbContext _context;
    private readonly IProductRepository _productRepository;
    public CreateProductCommandHandler(IAppDbContext context, IProductRepository productRepository)
        => (_context, _productRepository) = (context, productRepository);
    public async Task<ApiResult<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        bool productExists = await _productRepository.IsProductUniqueAsync(request.Name);

        if(productExists)
            return new ApiResult<ProductDTO>(new ProductDTO(), ResponseTypeEnum.Warning, "Product already exists.");

        Entities.Product entity = new()
        {	
            Description = request.Description,	
            Name = request.Name,	
            IsAvaliable = request.IsAvaliable,	
            Price = request.Price,	
            Quantity = request.Quantity	
        };
    
        entity.AddDomainEvent(new ProductCreateEvent(entity));
        _context.Products.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
    
        ProductDTO dto = new()
        {
            Id = entity.Id,
            Description = entity.Description,	
            Name = entity.Name,	
            IsAvaliable = entity.IsAvaliable,	
            Price = entity.Price,	
            Quantity = entity.Quantity
        };
    
        if (entity.Id <= 0)
            return new ApiResult<ProductDTO>(dto, ResponseTypeEnum.Error, "Error while trying to create the register.");
        
        return new ApiResult<ProductDTO>(dto, ResponseTypeEnum.Success, "Operation completed successfully."); 
    }
}