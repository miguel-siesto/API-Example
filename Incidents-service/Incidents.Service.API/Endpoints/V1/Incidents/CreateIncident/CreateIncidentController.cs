using Incidents.Service.API.Endpoints.V1.Incidents.GetAllIncidents;
using Incidents.Service.Core.Commands;
using Incidents.Service.Logic.Commands.CreateIncident;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Incidents.Service.API.Endpoints.V1.Incidents.CreateIncident;

[ApiController]
[Route("v1/incidents")]
public class CreateIncidentController(ICommandRunner commandRunner, ILogger<GetAllIncidentsController> logger) : ControllerBase
{
    [HttpPost]
    [Produces("application/json")]
    [SwaggerOperation(OperationId = "V1CreateIncident", Tags = ["Incidents"])]
    [SwaggerResponse(201, "Created")]
    [SwaggerResponse(400, "Bad Request.")]
    [SwaggerResponse(401, "Unauthorized. Caller is not authenticated.")]
    [SwaggerResponse(403, "Forbidden. AttachmentId is unknown or inactive, or caller does not have permissions.")]
    [SwaggerResponse(404, "AttachmentId not found.")]
    [SwaggerResponse(429, "Too many requests.")]
    public async Task<ActionResult> CreateAsync([FromBody] CreateIncidentCommand createIncidentCommand, CancellationToken cancellationToken)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await commandRunner.RunAsync(createIncidentCommand, cancellationToken);
            return Created();
        }
        catch (HttpRequestException ex)
        {
            var errorCode = ex.StatusCode ?? HttpStatusCode.InternalServerError;
            logger.LogWarning(ex, "[GetIncidentByIdController] - Error while processing request.");
            return StatusCode((int)errorCode, ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Internal error occurred while processing the request.");
            return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred.");
        }
    }
}
