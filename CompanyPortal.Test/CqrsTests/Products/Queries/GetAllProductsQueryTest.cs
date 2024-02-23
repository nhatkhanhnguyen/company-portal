using AutoMapper;

using CompanyPortal.CQRS.Products.Queries;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

using Moq;

namespace CompanyPortal.Test.CqrsTests.Products.Queries;

public class GetAllProductsQueryTest
{
    private readonly Mock<IMediator> _mockMediator = new();
    private readonly Mock<IRepository<Product>> _mockRepository = new();
    private readonly Mock<IUnitOfWork> _mockUow = new();
    private readonly IMapper _mapper;

    public GetAllProductsQueryTest()
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()))
            .CreateMapper();
    }

    [Fact]
    public async Task GetAllProductsQueryAsync_ShouldReturn()
    {
        _mockRepository
            .Setup(x => x.GetAllListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync([
                new Product { Id = 1, Name = "TEST PRODUCT 1" },
                new Product { Id = 2, Name = "TEST PRODUCT 2" },
                new Product { Id = 3, Name = "TEST PRODUCT 3" }
            ]);

        var query = new GetAllProductsQuery();
        var handler = new GetAllProductsQuery.Handler(_mockRepository.Object, _mapper);

        // Act
        var result = await handler.Handle(new GetAllProductsQuery(), CancellationToken.None);
        await _mockMediator.Object.Send(query, CancellationToken.None);
        
        // Assert
        _mockMediator.Verify(x => x.Send(It.IsAny<GetAllProductsQuery>(), CancellationToken.None), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
    }
}