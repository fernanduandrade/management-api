using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Common.Models;

namespace Shop.Application.Client.Commands;

public sealed record DeleteClientCommand :IRequest<ApiResult<bool>>
{
    public int Id { get; init; }
}

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, ApiResult<bool>>
{
    private readonly IAppDbContext _context;

    public DeleteClientCommandHandler(IAppDbContext context)
        => (_context) = (context);
    public async Task<ApiResult<bool>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Clients.FirstOrDefaultAsync(
            quiz => quiz.Id == request.Id);

        if (entity is null) return new ApiResult<bool>(false, "Erro ao deletar o registro");

        _context.Clients.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResult<bool>(true, "Operação concluida com sucesso.");
    }
}