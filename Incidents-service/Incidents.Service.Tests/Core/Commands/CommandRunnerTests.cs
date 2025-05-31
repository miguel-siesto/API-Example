using Incidents.Service.Core.Commands;
using Moq;

namespace Incidents.Service.Tests.Core.Commands;

public class CommandRunnerTests
{
    private Mock<IServiceProvider> _serviceProviderMock = null!;
    private CommandRunner _commandRunner = null!;

    [SetUp]
    public void Setup()
    {
        _serviceProviderMock = new Mock<IServiceProvider>();
        _commandRunner = new CommandRunner(_serviceProviderMock.Object);
    }

    public class DummyCommand { }

    [Test]
    public void RunAsync_ThrowsInvalidOperationException_WhenNoHandlerIsRegistered()
    {
        // Arrange
        var command = new DummyCommand();

        _serviceProviderMock
            .Setup(sp => sp.GetService(typeof(ICommandHandler<DummyCommand>)))
            .Returns((object?)null);

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(
            () => _commandRunner.RunAsync(command, CancellationToken.None));

        Assert.That(ex!.Message, Is.EqualTo("No handler registered for query type DummyCommand"));
    }

    [Test]
    public async Task RunAsync_CallsHandlerExecuteAsync_WhenHandlerIsRegistered()
    {
        // Arrange
        var command = new DummyCommand();
        var handlerMock = new Mock<ICommandHandler<DummyCommand>>();

        _serviceProviderMock
            .Setup(sp => sp.GetService(typeof(ICommandHandler<DummyCommand>)))
            .Returns(handlerMock.Object);

        // Act
        await _commandRunner.RunAsync(command, CancellationToken.None);

        // Assert
        handlerMock.Verify(h => h.ExecuteAsync(command, CancellationToken.None), Times.Once);
    }
}
