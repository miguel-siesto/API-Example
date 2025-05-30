namespace Incidents.Service.Logic.Queries.GetIncidentById;

using Incidents.Service.Core.Queries;

/// <summary>Interface for GetIncidentById query.</summary>
public class GetIncidentByIdQuery : IQuery
{
    /// <summary>Gets or sets the incident identifier.</summary>
    /// <value>The incident identifier.</value>
    public Guid IncidentId { get; set; }
}
