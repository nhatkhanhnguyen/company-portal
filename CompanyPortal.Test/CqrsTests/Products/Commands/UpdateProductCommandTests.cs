using CompanyPortal.CQRS.Products.Commands;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.Test.Common;
using CompanyPortal.ViewModels;

using Moq;

namespace CompanyPortal.Test.CqrsTests.Products.Commands;

public class UpdateProductCommandTests : TestsBase<Product>
{
    [Fact]
    public async Task UpdateProductCommand_ShouldUpdate()
    {
        _mockRepository.Setup(x => x.Update(It.IsAny<Product>()));
        _mockUow.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(true);
        var product = new ProductViewModel();
        var command = new UpdateProductCommand(product);
        var commandHandler = new UpdateProductCommand.Handler(_mapper, _mockRepository.Object, _mockUow.Object);

        await _mockMediator.Object.Send(command);
        var result = await commandHandler.Handle(command, CancellationToken.None);

        _mockMediator.Verify(x => x.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockUow.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        Assert.True(result > 0);
    }
}