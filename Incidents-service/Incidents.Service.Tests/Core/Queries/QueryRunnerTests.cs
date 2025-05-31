using Incidents.Service.Core.Queries;
using Moq;

namespace Incidents.Service.Tests.Core.Queries;

public class QueryRunnerTests
{
    private Mock<IServiceProvider> _serviceProviderMock = null!;
    private QueryRunner _queryRunner = null!;

    [SetUp]
    public void Setup()
    {
        _serviceProviderMock = new Mock<IServiceProvider>();
        _queryRunner = new QueryRunner(_serviceProviderMock.Object);
    }

    public class DummyQuery { }

    [Test]
    public void RunAsync_ThrowsInvalidOperationException_WhenNoHandlerIsRegistered()
    {
        // Arrange
        var query = new DummyQuery();

        _serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IQueryHandler<DummyQuery, string>)))
            .Returns((object?)null);

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(
            () => _queryRunner.RunAsync<DummyQuery, string>(query, CancellationToken.None));

        Assert.That(ex!.Message, Is.EqualTo("No handler registered for query type DummyQuery"));
    }

    [Test]
    public async Task RunAsync_CallsHandlerExecuteAsyncAndReturnsResult_WhenHandlerIsRegistered()
    {
        // Arrange
        var query = new DummyQuery();
        var expectedResult = "ExpectedResult";
        var handlerMock = new Mock<IQueryHandler<DummyQuery, string>>();

        handlerMock
            .Setup(h => h.ExecuteAsync(query, CancellationToken.None))
            .ReturnsAsync(expectedResult);

        _serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IQueryHandler<DummyQuery, string>)))
            .Returns(handlerMock.Object);

        // Act
        var result = await _queryRunner.RunAsync<DummyQuery, string>(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
        handlerMock.Verify(h => h.ExecuteAsync(query, CancellationToken.None), Times.Once);
    }
}
