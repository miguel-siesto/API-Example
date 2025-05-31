using Incidents.Service.Core.Queries;
using Incidents.Service.Data.DataTransferObjects;
using Incidents.Service.Logic.Queries.GetAllIncidents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Incidents.Service.API.Endpoints.V1.Incidents.GetAllIncidents;

[ApiController]
[Route("v1/incidents")]
public class GetAllIncidentsController(IQueryRunner queryRunner, ILogger<GetAllIncidentsController> logger) : ControllerBase
{
    [HttpGet]
    [Produces("application/json")]
    [SwaggerOperation(OperationId = "V1GetAllIncidents", Tags = ["Incidents"])]
    [SwaggerResponse(200, "OK")]
    [SwaggerResponse(400, "Bad Request.")]
    [SwaggerResponse(401, "Unauthorized. Caller is not authenticated.")]
    [SwaggerResponse(403, "Forbidden. AttachmentId is unknown or inactive, or caller does not have permissions.")]
    [SwaggerResponse(404, "AttachmentId not found.")]
    [SwaggerResponse(429, "Too many requests.")]
    public async Task<ActionResult> GetAsync([FromQuery] GetAllIncidentsQuery getIncidentQuery, CancellationToken cancellationToken)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var queryResponse = await queryRunner.RunAsync<GetAllIncidentsQuery, IEnumerable<IncidentDto>>(getIncidentQuery, cancellationToken);
            return Ok(queryResponse);
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
