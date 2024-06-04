using MediatR;
using Shop.Application.Shared;
using Shop.Domain.Products;

namespace Shop.Application.Reports.GetProducts;


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
