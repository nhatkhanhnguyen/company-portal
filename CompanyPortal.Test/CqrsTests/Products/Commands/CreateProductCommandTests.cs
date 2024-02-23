using CompanyPortal.CQRS.Products.Commands;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.Test.Common;
using CompanyPortal.ViewModels;
using Moq;

namespace CompanyPortal.Test.CqrsTests.Products.Commands;

public class CreateProductCommandTests : TestsBase<Product>
{
    [Fact]
    public async Task CreateProductCommand_ShouldCreate()
    {
        _mockRepository.Setup(x => x.InsertAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _mockUow.Setup(x => x.SaveChangesAsync()).ReturnsAsync(true);
        var product = new ProductViewModel();
        var command = new CreateProductCommand(product);
        var commandHandler = new CreateProductCommand.Handler(_mapper, _mockRepository.Object, _mockUow.Object);

        await _mockMediator.Object.Send(command);
        var result = await commandHandler.Handle(command, CancellationToken.None);

        _mockMediator.Verify(x => x.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockUow.Verify(x => x.SaveChangesAsync(), Times.Once);
        Assert.True(result);
    }
}