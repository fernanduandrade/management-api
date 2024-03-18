using MediatR;
using Shop.Application.Common.Models;

namespace Shop.Application.Clients.AddInput;

public sealed record AddInputCommand(Guid id, InputType type, decimal value)
  : IRequest<ApiResult<Unit>> {}


public enum InputType {
  Debito,
  Credito
}