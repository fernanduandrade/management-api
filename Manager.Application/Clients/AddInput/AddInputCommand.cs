using MediatR;
using Manager.Application.Common.Models;

namespace Manager.Application.Clients.AddInput;

public sealed record AddInputCommand(Guid id, InputType type, decimal value)
  : IRequest<ApiResult<Unit>> {}


public enum InputType {
  Debito,
  Credito
}