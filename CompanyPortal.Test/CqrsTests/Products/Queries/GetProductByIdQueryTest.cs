using AutoMapper;

using CompanyPortal.CQRS.Products.Queries;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;

using MediatR;

using Moq;

namespace CompanyPortal.Test.CqrsTests.Products.Queries;

public class GetProductByIdQueryTest
{
    private readonly Mock<IMediator> _mockMediator = new();
    private readonly Mock<IRepository<Product>> _mockRepository = new();
    private readonly Mock<IUnitOfWork> _mockUow = new();
    private readonly IMapper _mapper;

    public GetProductByIdQueryTest()
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()))
            .CreateMapper();
    }

    [Fact]
    public async Task GetProductByIdQueryAsync_ShouldReturn()
    {
        _mockRepository
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Product { Id = 1, Name = "TEST PRODUCT 1" });

        var query = new GetProductByIdQuery(1);
        var handler = new GetProductByIdQuery.Handler(_mockRepository.Object, _mapper);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        await _mockMediator.Object.Send(query, CancellationToken.None);

        // Assert
        _mockMediator.Verify(x => x.Send(It.IsAny<GetProductByIdQuery>(), CancellationToken.None), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }
    
    [Fact]
    public async Task GetProductByIdQueryAsync_ShouldReturnNull()
    {
        _mockRepository
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(default(Product));

        var query = new GetProductByIdQuery(1);
        var handler = new GetProductByIdQuery.Handler(_mockRepository.Object, _mapper);

        // Act
        await _mockMediator.Object.Send(query, CancellationToken.None);
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        _mockMediator.Verify(x => x.Send(It.IsAny<GetProductByIdQuery>(), CancellationToken.None), Times.Once);
        Assert.Null(result);
    }
}