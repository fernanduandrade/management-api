using AutoMapper;
using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Product.DTOs;
using Shop.Application.Product.Interfaces;
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
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResult<ProductDTO>>
{
    private readonly IAppDbContext _context;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IAppDbContext context, IProductRepository productRepository, IMapper mapper)
        => (_context, _productRepository, _mapper) = (context, productRepository, mapper);

    public async Task<ApiResult<ProductDTO>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _productRepository.FindByIdAsync(request.Id);

            if (entity is null)
                return new ApiResult<ProductDTO>(new ProductDTO(), ResponseTypeEnum.Warning,
                    "Failed to update the record, product not found.");

            
            Entities.Product updateEntity = new()
            {
                Description = request.Description,
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity,
            };

            updateEntity.AddDomainEvent(new ProductCreateEvent(updateEntity));
            _productRepository.SetEntityStateModified(updateEntity);
            await _context.SaveChangesAsync(cancellationToken);

            ProductDTO dto = _mapper.Map<ProductDTO>(updateEntity);

            return new ApiResult<ProductDTO>(dto, ResponseTypeEnum.Success, "Operation completed successfully.");
        
    }
}
