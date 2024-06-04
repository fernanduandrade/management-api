using Manager.Domain.Products;

namespace Manager.Application.Reports.GetProducts;

public sealed record ProductsReportModel(
    string IssuedBy,
    List<Product> Products
);
