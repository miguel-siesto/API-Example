using Incidents.Service.Core.Enums;
using Incidents.Service.Data.DataTransferObjects;
using Incidents.Service.Data.Repositories;
using Incidents.Service.Logic.Queries.GetIncidentById;
using Moq;

namespace Incidents.Service.Tests.Logic.Queries;

public class GetIncidentByIdQueryHandlerTests
{
    private Mock<IIncidentRepository> _incidentRepositoryMock = null!;
    private GetIncidentByIdQueryHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _incidentRepositoryMock = new Mock<IIncidentRepository>();
        _handler = new GetIncidentByIdQueryHandler(_incidentRepositoryMock.Object);
    }

    [Test]
    public void TimeoutAfter_ShouldBe3000Milliseconds()
    {
        Assert.Multiple(() =>
        {
            // Act
            var timeout = _handler.TimeoutAfter;

            // Assert
            Assert.That(timeout.TotalMilliseconds, Is.EqualTo(3000));
        });
    }

    [Test]
    public async Task ExecuteAsync_ReturnsIncident_WhenIncidentExists()
    {
        // Arrange
        var incidentId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;
        var incident = new IncidentDto(incidentId, timestamp, IncidentSeverity.Major, "Found Incident");
        var query = new GetIncidentByIdQuery { IncidentId = incidentId };

        _incidentRepositoryMock
            .Setup(repo => repo.GetIncident(incidentId))
            .Returns(incident);

        // Act
        var result = await _handler.ExecuteAsync(query, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.IncidentId, Is.EqualTo(incidentId));
            Assert.That(result.Timestamp, Is.EqualTo(timestamp));
            Assert.That(result.Severity, Is.EqualTo(IncidentSeverity.Major));
            Assert.That(result.Description, Is.EqualTo("Found Incident"));
        });
    }

    [Test]
    public void ExecuteAsync_ThrowsHttpRequestException_WhenIncidentNotFound()
    {
        // Arrange
        var incidentId = Guid.NewGuid();
        var query = new GetIncidentByIdQuery { IncidentId = incidentId };

        _incidentRepositoryMock
            .Setup(repo => repo.GetIncident(incidentId))
            .Returns((IncidentDto?)null);

        // Act & Assert
        Assert.Multiple(() =>
        {
            var ex = Assert.ThrowsAsync<HttpRequestException>(() => _handler.ExecuteAsync(query, CancellationToken.None));
            Assert.That(ex!.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
            Assert.That(ex.Message, Does.Contain($"Incident with ID {incidentId} not found."));
        });
    }
}
