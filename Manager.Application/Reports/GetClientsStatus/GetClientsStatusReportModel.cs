using MediatR;
using Manager.Domain.Clients;
namespace Manager.Application.Reports.GetClientsStatus;

public sealed record GetClientsStatusReportModel(
    List<Client> Clients,
    string? IssuedBy
);
