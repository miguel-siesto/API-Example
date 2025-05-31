using Incidents.Service.Core.Enums;
using Incidents.Service.Data.DataTransferObjects;
using Incidents.Service.Data.Repositories;
using Incidents.Service.Logic.Commands.CreateIncident;
using Moq;

namespace Incidents.Service.Tests.Logic.Commands;

public class CreateIncidentCommandHandlerTests
{
    private Mock<IIncidentRepository> _incidentRepositoryMock = null!;
    private CreateIncidentCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _incidentRepositoryMock = new Mock<IIncidentRepository>();
        _handler = new CreateIncidentCommandHandler(_incidentRepositoryMock.Object);
    }

    [Test]
    public void TimeoutAfter_ShouldBe3000Milliseconds()
    {
        // Act
        var timeout = _handler.TimeoutAfter;

        // Assert
        Assert.That(timeout.TotalMilliseconds, Is.EqualTo(3000));
    }

    [Test]
    public async Task ExecuteAsync_CallsAddIncidentWithCorrectDto()
    {
        // Arrange
        var command = new CreateIncidentCommand() { IncidentId = Guid.NewGuid(), Timestamp = DateTime.UtcNow, Severity = IncidentSeverity.Minor, Description = "Test incidency." };
        var expectedDto = command.ToIncidentDto();

        // Act
        await _handler.ExecuteAsync(command, CancellationToken.None);

        // Assert
        _incidentRepositoryMock.Verify(repo =>
            repo.AddOrUpdateIncident(It.Is<IncidentDto>(dto =>
                dto.Description == expectedDto.Description &&
                dto.Severity == expectedDto.Severity &&
                dto.Timestamp == expectedDto.Timestamp &&
                dto.IncidentId == expectedDto.IncidentId
            )),
            Times.Once);
            }
}
