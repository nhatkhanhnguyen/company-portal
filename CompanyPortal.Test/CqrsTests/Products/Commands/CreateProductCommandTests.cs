using CompanyPortal.CQRS.Products.Commands;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.Test.Common;
using CompanyPortal.ViewModels;

using Microsoft.Extensions.Logging;

using Moq;

namespace CompanyPortal.Test.CqrsTests.Products.Commands;

public class CreateProductCommandTests : TestsBase<Product>
{
    protected readonly Mock<ILogger<CreateProductCommand.Handler>> _mockLogger = new();

    [Fact]
    public async Task CreateProductCommand_ShouldCreate()
    {
        _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _mockUow.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(true);
        var product = new ProductViewModel();
        var command = new CreateProductCommand(product);
        var commandHandler = new CreateProductCommand.Handler(_mockRepository.Object, _mockUow.Object, _mapper, _mockLogger.Object);

        await _mockMediator.Object.Send(command);
        var result = await commandHandler.Handle(command, CancellationToken.None);

        _mockMediator.Verify(x => x.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockUow.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        Assert.True(result.IsSuccess);
    }
}