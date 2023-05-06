using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;

namespace Shop.Application.Sale.Commands;

public sealed record DeleteSaleCommand : IRequest<ApiResult>
{
    public int Id { get; init; }
}

public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, ApiResult>
{
    private readonly IAppDbContext _context;

    public DeleteSaleCommandHandler(IAppDbContext context)
        => (_context) = (context);
    public async Task<ApiResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sales.FirstOrDefaultAsync(
            sale => sale.Id == request.Id);

        if (entity is null) return new ApiResult("Error while trying to delete the register.", ResponseTypeEnum.Error);

        _context.Sales.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResult("Operation completed successfully.", ResponseTypeEnum.Success);
    }
}