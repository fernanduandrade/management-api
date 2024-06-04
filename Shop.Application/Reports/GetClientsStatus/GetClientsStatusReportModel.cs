using MediatR;
using Shop.Domain.Clients;
namespace Shop.Application.Reports.GetClientsStatus;

public sealed record GetClientsStatusReportModel(
    List<Client> Clients,
    string? IssuedBy
);
