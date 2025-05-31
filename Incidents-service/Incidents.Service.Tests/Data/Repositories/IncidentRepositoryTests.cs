using Incidents.Service.Core.Enums;
using Incidents.Service.Data.DataTransferObjects;
using Incidents.Service.Data.Repositories;

namespace Incidents.Service.Tests.Data.Repositories;

public class IncidentRepositoryTests
{
    private IncidentRepository _repository = null!;

    [SetUp]
    public void Setup()
    {
        _repository = new IncidentRepository();
    }

    [Test]
    public void AddOrUpdateIncident_ShouldAddNewIncident()
    {
        // Arrange
        var incident = new IncidentDto
        (
            Guid.NewGuid(),
            DateTime.UtcNow,
            IncidentSeverity.Minor,
            "Test incident"
        );

        // Act
        _repository.AddOrUpdateIncident(incident);
        var result = _repository.GetIncident(incident.IncidentId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.IncidentId, Is.EqualTo(incident.IncidentId));
            Assert.That(result.Description, Is.EqualTo("Test incident"));
        });
    }

    [Test]
    public void AddOrUpdateIncident_ShouldUpdateExistingIncident()
    {
        // Arrange
        var id = Guid.NewGuid();
        var original = new IncidentDto(id, DateTime.UtcNow, IncidentSeverity.Minor, "Original");
        var updated = new IncidentDto(id, DateTime.UtcNow, IncidentSeverity.Critical, "Updated");

        // Act
        _repository.AddOrUpdateIncident(original);
        _repository.AddOrUpdateIncident(updated);
        var result = _repository.GetIncident(id);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Description, Is.EqualTo("Updated"));
            Assert.That(result.Severity, Is.EqualTo(IncidentSeverity.Critical));
        });
    }

    [Test]
    public void GetIncident_ShouldReturnNullIfNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var result = _repository.GetIncident(id);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetIncidents_ShouldReturnAllAddedIncidents()
    {
        // Arrange
        var incident1 = new IncidentDto(Guid.NewGuid(), DateTime.UtcNow, IncidentSeverity.Major, "First");
        var incident2 = new IncidentDto(Guid.NewGuid(), DateTime.UtcNow, IncidentSeverity.Minor, "Second");

        _repository.AddOrUpdateIncident(incident1);
        _repository.AddOrUpdateIncident(incident2);

        // Act
        var incidents = _repository.GetIncidents().ToList();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(incidents, Has.Count.EqualTo(2));
            Assert.That(incidents.Any(i => i.IncidentId == incident1.IncidentId), Is.True);
            Assert.That(incidents.Any(i => i.IncidentId == incident2.IncidentId), Is.True);
        });
    }
}
