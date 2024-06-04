// using AutoMapper;
// using Moq;
// using Manager.Application.Common.Mapping;
// using Manager.Application.Product.Interfaces;
// using Manager.Application.Product.Queries;
// using Manager.Application.UnitTest.Mocks;
// using Shouldly;
//
// namespace Manager.Application.UnitTest.Products.Queries;
//
// public class GetAllProductQueryHandlerTest
// {
//     private readonly IMapper _mapper;
//     private readonly Mock<IProductRepository> _productRepository;
//
//     public GetAllProductQueryHandlerTest()
//     {
//         _productRepository = MockProductRepository.GetProductRepository();
//         var mapperConfig = new MapperConfiguration(c =>
//         {
//             c.AddProfile<MappingProfile>();
//         });
//
//         _mapper = mapperConfig.CreateMapper();
//     }
//
//     [Fact]
//     public async Task GetAll_Should_Return_Count_4()
//     {
//         var handler = new GetAllProductPaginatedQueryHandler(
//             _productRepository.Object,
//             _mapper
//             );
//
//         var result = await handler.Handle(new GetAllProductPaginatedQuery() { PageNumber = 1, PageSize = 20 }, CancellationToken.None);
//         
//         result.Data.Items.Count.ShouldBe(16);
//     }
//     
//     [Fact]
//     public async Task GetAll_When_No_Results_Should_Return_Empty_List()
//     {
//         var mockRepo = new Mock<IProductRepository>();
//         
//         var handler = new GetAllProductPaginatedQueryHandler(
//             mockRepo.Object,
//             _mapper
//         );
//
//         var result = await handler.Handle(new GetAllProductPaginatedQuery() { PageNumber = 1, PageSize = 20 }, CancellationToken.None);
//         
//         result.Data.Items.Count.ShouldBe(0);
//     }
//     
//     [Fact]
//     public async Task GetAll_Pagination_Should_Have_Next_Page()
//     {
//         var mockProducts = MockProductRepository.GetProductData();
//         var mockRepository = new Mock<IProductRepository>();
//         mockRepository
//             .Setup(x => x.GetAllPaginated(5, 1))
//             .ReturnsAsync(mockProducts);
//         
//         var handler = new GetAllProductPaginatedQueryHandler(
//             mockRepository.Object,
//             _mapper
//         );
//
//         var result = await handler.Handle(new GetAllProductPaginatedQuery() { PageNumber = 1, PageSize = 5 }, CancellationToken.None);
//         
//         result.Data.HasNextPage.ShouldBeTrue();
//     }
//     
//     [Fact]
//     public async Task GetAll_Pagination_Should_Have_Previous_Page()
//     {
//         var mockProducts = MockProductRepository.GetProductData();
//         var mockRepository = new Mock<IProductRepository>();
//         mockRepository
//             .Setup(x => x.GetAllPaginated(5, 1))
//             .ReturnsAsync(mockProducts);
//         
//         var handler = new GetAllProductPaginatedQueryHandler(
//             mockRepository.Object,
//             _mapper
//         );
//
//         var result = await handler.Handle(new GetAllProductPaginatedQuery() { PageNumber = 3, PageSize = 5 }, CancellationToken.None);
//         
//         result.Data.HasPreviousPage.ShouldBeTrue();
//     }
//     
//     [Fact]
//     public async Task GetAll_Pagination_Should_TotalPage_Equal_Four()
//     {
//         var mockProducts = MockProductRepository.GetProductData();
//         var mockRepository = new Mock<IProductRepository>();
//         mockRepository
//             .Setup(x => x.GetAllPaginated(5, 1))
//             .ReturnsAsync(mockProducts);
//         
//         var handler = new GetAllProductPaginatedQueryHandler(
//             mockRepository.Object,
//             _mapper
//         );
//
//         var result = await handler.Handle(new GetAllProductPaginatedQuery() { PageNumber = 1, PageSize = 5 }, CancellationToken.None);
//         
//         result.Data.TotalPages.ShouldBe(4);
//     }
// }