using MediatR;
using Manager.Application.Shared;
using Manager.Domain.Clients;

namespace Manager.Application.Reports.GetClientsStatus;

public sealed record GetClientsStatusQuery() : IRequest<Stream> {}

public class GetClientsStatusQueryHandler(IClientRepository repository)
  : IRequestHandler<GetClientsStatusQuery, Stream>
{
    public async Task<Stream> Handle(GetClientsStatusQuery request, CancellationToken cancellationToken)
    {
        var records = repository.Get();
        var reportModel = new GetClientsStatusReportModel(records.ToList(), "");

        return await Helper.CreateReport(reportModel, "Clients", "ClientStatus.cshtml");
    }
}
