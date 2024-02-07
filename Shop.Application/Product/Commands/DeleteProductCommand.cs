using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;
using Shop.Application.Product.Interfaces;

namespace Shop.Application.Product.Commands;

public sealed record DeleteProductCommand : IRequest<ApiResult>
{
    public Guid Id { get; init; }
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResult>
{
    private readonly IAppDbContext _context;
    private readonly IProductRepository _productRepository;
    
    public DeleteProductCommandHandler(IAppDbContext context, IProductRepository productRepository)
        => (_context, _productRepository) = (context, productRepository);
    public async Task<ApiResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _productRepository.FindByIdAsync(request.Id);

        if (entity is null) return new ApiResult(false, ResponseTypeEnum.Error, "Error while trying to delete the register.");

        _context.Products.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResult(true, ResponseTypeEnum.Success, "Operation completed successfully.");
    }
}