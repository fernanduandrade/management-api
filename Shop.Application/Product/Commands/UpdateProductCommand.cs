using AutoMapper;
using MediatR;
using Shop.Application.Common.Models;
using Shop.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Mapping;
using Shop.Application.Product.DTOs;
using Entities = Shop.Domain.Entities;
using Shop.Domain.Events;

namespace Shop.Application.Product.Commands;

public sealed record UpdateProductCommand : IRequest<ApiResult<ProductDTO>>, IMapFrom<Entities.Product>
{
    public int Id {get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Entities.Product, UpdateProductCommand>();
    }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResult<ProductDTO>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IAppDbContext context, IMapper mapper)
        => (_context, _mapper) = (context, mapper);

    public async Task<ApiResult<ProductDTO>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context
            .Products
            .AsNoTracking()
            .FirstOrDefaultAsync(product => product.Id == request.Id);

        if(entity is null)
            return new ApiResult<ProductDTO>(_mapper.Map<ProductDTO>(request), ResponseTypeEnum.Warning, "Failed to update the register.");

        var updateEntity = _mapper.Map<Entities.Product>(request);

        updateEntity.AddDomainEvent(new ProductCreateEvent(updateEntity));
        _context.Products.Entry(updateEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);

        ProductDTO dto = _mapper.Map<ProductDTO>(updateEntity);
        
        return new ApiResult<ProductDTO>(dto, ResponseTypeEnum.Success ,"Operation completed successfully.");
    }
}
