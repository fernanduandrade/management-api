using AutoMapper;
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
    private readonly IMapper _mapper;
    public CreateProductCommandHandler(IAppDbContext context, IProductRepository productRepository, IMapper mapper)
        => (_context, _productRepository, _mapper) = (context, productRepository, mapper);
    public async Task<ApiResult<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        bool productExists = await _productRepository.IsProductUniqueAsync(request.Name);

        if(productExists)
            return new ApiResult<ProductDTO>(new ProductDTO(), ResponseTypeEnum.Warning, "Product already exists.");

        Entities.Product entity = new()
        {	
            Description = request.Description,	
            Name = request.Name,
            Price = request.Price,	
            Quantity = request.Quantity	
        };
    
        entity.AddDomainEvent(new ProductCreateEvent(entity));
        _context.Products.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        if (entity.Id <= 0)
            return new ApiResult<ProductDTO>(null, ResponseTypeEnum.Error, "Error while trying to create the register.");
        
        ProductDTO dto = _mapper.Map<ProductDTO>(entity);
        
        return new ApiResult<ProductDTO>(dto, ResponseTypeEnum.Success, "Operation completed successfully."); 
    }
}