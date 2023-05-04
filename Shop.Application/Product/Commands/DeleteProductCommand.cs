using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;

namespace Shop.Application.Product.Commands;

public record DeleteProductCommand : IRequest<ApiResult<bool>>
{
    public int Id { get; init; }
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResult<bool>>
{
    private readonly IAppDbContext _context;
    
    public DeleteProductCommandHandler(IAppDbContext context)
        => (_context) = (context);
    public async Task<ApiResult<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products.FirstOrDefaultAsync(
            quiz => quiz.Id == request.Id);

        if (entity is null) return new ApiResult<bool>(false, "Erro ao deletar o registro");

        _context.Products.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResult<bool>(true, "Operação concluida com sucesso.");
    }
}