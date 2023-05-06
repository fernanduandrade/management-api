using AutoMapper;
using MediatR;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Mapping;
using Shop.Application.Common.Models;
using Shop.Application.Product.DTOs;
using Shop.Domain.Events;
using Entities = Shop.Domain.Entities;

namespace Shop.Application.Product.Commands;

public sealed record CreateProductCommand : IMapFrom<Entities.Product>, IRequest<ApiResult<ProductDTO>>
{
    public string Name { get; init; }
    public string Description { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
    public bool IsAvaliable { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Entities.Product, CreateProductCommand>();
    }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResult<ProductDTO>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    public CreateProductCommandHandler(IAppDbContext context, IMapper mapper)
        => (_context, this._mapper) = (context, mapper);
    public async Task<ApiResult<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Entities.Product>(request);
   
        entity.AddDomainEvent(new ProductCreateEvent(entity));
        _context.Products.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        ProductDTO dto = _mapper.Map<ProductDTO>(entity);
        
        if (entity.Id <= 0)
            return new ApiResult<ProductDTO>(dto, ResponseTypeEnum.Error, "Error while trying to create the register.");
        
        return new ApiResult<ProductDTO>(dto, ResponseTypeEnum.Success, "Operation completed successfully.");
    }
}