using Incidents.Service.Core.Enums;
using Incidents.Service.Data.DataTransferObjects;
using Incidents.Service.Data.Repositories;
using Incidents.Service.Logic.Queries.GetAllIncidents;
using Moq;

namespace Incidents.Service.Tests.Logic.Queries;

public class GetAllIncidentsQueryHandlerTests
{
    private Mock<IIncidentRepository> _incidentRepositoryMock = null!;
    private GetAllIncidentsQueryHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _incidentRepositoryMock = new Mock<IIncidentRepository>();
        _handler = new GetAllIncidentsQueryHandler(_incidentRepositoryMock.Object);
    }

    [Test]
    public void TimeoutAfter_ShouldBe3000Milliseconds()
    {
        Assert.Multiple(() =>
        {
            var timeout = _handler.TimeoutAfter;
            Assert.That(timeout.TotalMilliseconds, Is.EqualTo(3000));
        });
    }

    [Test]
    public async Task ExecuteAsync_ReturnsAllIncidents_WhenIncidentsExist()
    {
        // Arrange
        var incidents = new List<IncidentDto>
           {
               new(Guid.NewGuid(), DateTime.UtcNow, IncidentSeverity.Minor, "Incident 1"),
               new(Guid.NewGuid(), DateTime.UtcNow, IncidentSeverity.Major, "Incident 2"),
           };

        _incidentRepositoryMock
            .Setup(repo => repo.GetIncidents())
            .Returns(incidents);

        var query = new GetAllIncidentsQuery();

        // Act
        var result = await _handler.ExecuteAsync(query, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Count(), Is.EqualTo(2));
            Assert.That(result, Is.EquivalentTo(incidents));
        });
    }

    [Test]
    public async Task ExecuteAsync_ReturnsNull_WhenRepositoryReturnsNull()
    {
        // Arrange  
        _incidentRepositoryMock
            .Setup(repo => repo.GetIncidents())
            .Returns((IEnumerable<IncidentDto>?)null!);

        var query = new GetAllIncidentsQuery();

        // Act  
        var result = await _handler.ExecuteAsync(query, CancellationToken.None);

        // Assert  
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
        });
    }
}
