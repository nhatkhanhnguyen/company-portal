using CompanyPortal.CQRS.Products.Commands;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.Test.Common;
using CompanyPortal.ViewModels;

using Microsoft.Extensions.Logging;

using Moq;

namespace CompanyPortal.Test.CqrsTests.Products.Commands;

public class UpdateProductCommandTests : TestsBase<Product>
{
    protected readonly Mock<ILogger<UpdateProductCommand.Handler>> _mockLogger = new();

    [Fact]
    public async Task UpdateProductCommand_ShouldUpdate()
    {
        _mockRepository.Setup(x => x.Update(It.IsAny<Product>()));
        var mockResourceRepository = new Mock<IRepository<Resource>>();
        mockResourceRepository.Setup(x => x.Update(It.IsAny<Resource>()));
        _mockUow.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(true);
        var product = new ProductViewModel();
        var command = new UpdateProductCommand(product);
        var commandHandler = new UpdateProductCommand.Handler(_mockRepository.Object, mockResourceRepository.Object, _mockUow.Object, _mapper, _mockLogger.Object);

        await _mockMediator.Object.Send(command);
        var result = await commandHandler.Handle(command, CancellationToken.None);

        _mockMediator.Verify(x => x.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockUow.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        Assert.True(result.IsSuccess);
    }
}