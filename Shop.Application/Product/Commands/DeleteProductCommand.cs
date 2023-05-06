using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;

namespace Shop.Application.Product.Commands;

public sealed record DeleteProductCommand : IRequest<ApiResult>
{
    public int Id { get; init; }
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResult>
{
    private readonly IAppDbContext _context;
    
    public DeleteProductCommandHandler(IAppDbContext context)
        => (_context) = (context);
    public async Task<ApiResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products.FirstOrDefaultAsync(
            quiz => quiz.Id == request.Id);

        if (entity is null) return new ApiResult("Error while trying to delete the register.", ResponseTypeEnum.Error);

        _context.Products.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResult("Operation completed successfully.", ResponseTypeEnum.Success);
    }
}