using Incidents.Service.Core.Attributes;
using Incidents.Service.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Incidents.Service.Logic.Queries.GetIncidentById;

/// <summary>Interface for GetIncidentById query.</summary>
public class GetIncidentByIdQuery : IQuery
{
    /// <summary>Gets or sets the incident identifier.</summary>
    /// <value>The incident identifier.</value>
    [FromRoute(Name = "incidentid")]
    [NotEmptyGuid(ErrorMessage = "IncidentId is required.")]
    public Guid IncidentId { get; set; }
}
