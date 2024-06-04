using MediatR;
using Shop.Application.Shared;
using Shop.Domain.Clients;

namespace Shop.Application.Reports.GetClientsStatus;

public sealed record GetClientsStatusQuery() : IRequest<Stream> {}

public class GetClientsStatusQueryHandler(IClientRepository repository)
  : IRequestHandler<GetClientsStatusQuery, Stream>
{
    public async Task<Stream> Handle(GetClientsStatusQuery request, CancellationToken cancellationToken)
    {
        var records = repository.GetAll();
        var reportModel = new GetClientsStatusReportModel(records.ToList(), "");

        return await Helper.CreateReport(reportModel, "Clients", "ClientStatus.cshtml");
    }
}
