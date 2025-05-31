using Incidents.Service.Core.Queries;
using Incidents.Service.Logic.Queries.GetIncidentById;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Text.Json;

namespace Incidents.Service.API.Endpoints.V1.Incidents.GetIncidentById;

[ApiController]
[Route("v1/incidents/{incidentid}")]
public class GetIncidentByIdController(IQueryRunner queryRunner, ILogger<GetIncidentByIdController> logger) : ControllerBase
{
    [HttpGet]
    [Produces("application/json")]
    [SwaggerOperation(OperationId = "V1GetIncidentById", Tags = ["Incidents"])]
    [SwaggerResponse(200, "OK")]
    [SwaggerResponse(400, "Bad Request.")]
    [SwaggerResponse(401, "Unauthorized. Caller is not authenticated.")]
    [SwaggerResponse(403, "Forbidden. AttachmentId is unknown or inactive, or caller does not have permissions.")]
    [SwaggerResponse(404, "AttachmentId not found.")]
    [SwaggerResponse(429, "Too many requests.")]
    public async Task<ActionResult> GetAsync([FromRoute] GetIncidentByIdQuery getInvoiceQuery, CancellationToken cancellationToken)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var queryResponse = await queryRunner.RunAsync<GetIncidentByIdQuery, JsonDocument>(getInvoiceQuery, cancellationToken);
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
