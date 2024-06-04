using Shop.Domain.Products;

namespace Shop.Application.Reports.GetProducts;

public sealed record ProductsReportModel(
    string IssuedBy,
    List<Product> Products
);
