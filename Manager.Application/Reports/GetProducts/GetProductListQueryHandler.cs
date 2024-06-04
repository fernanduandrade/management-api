using MediatR;
using Manager.Application.Shared;
using Manager.Domain.Products;

namespace Manager.Application.Reports.GetProducts;


public sealed record GetProductListQuery() : IRequest<Stream> {}

public class GetProductListQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductListQuery, Stream>
{
    public async Task<Stream> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var records = productRepository.Get();
        var reportModel = new ProductsReportModel("", records.ToList());

        return await Helper.CreateReport(reportModel, "Products", "ListProducts.cshtml");
    }
}
