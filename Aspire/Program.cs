var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Shop_WebApi>("web-api");
builder.Build().Run();