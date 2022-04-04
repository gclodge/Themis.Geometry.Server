using Microsoft.AspNetCore.Mvc;

using Themis.Geometry.Server.Commands;
using Themis.Geometry.Server.Services;

using MediatR;
using Serilog;

namespace Themis.Geometry.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectionController : ControllerBase
    {
        private object projectionLock = ProjectionService.ProjectionLock;

        private readonly IMediator mediator;

        public ProjectionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("wkt")]
        public string? GetProjectionWkt()
        {
            var command = new GetProjectionWktCommand(ref projectionLock);
            var result = this.mediator.Send(command).Result;
            Log.Information("[ProjectionController] Sent GetProjectionWkt request");
            return result;
        }

        [HttpPut("wkt")]
        public IActionResult SetProjectionWkt([FromBody] string wkt)
        {
            var command = new SetProjectionWktCommand(wkt, ref projectionLock);
            this.mediator.Send(command).Wait();
            Log.Information($"[ProjectionController] Sent SetProjectionWkt request with WKT: {wkt}");
            return Ok();
        }

        [HttpGet("epsg")]
        public int? GetProjectionEpsgCode()
        {
            var command = new GetProjectionEpsgCodeCommand(ref projectionLock);
            var result = this.mediator.Send(command).Result;
            Log.Information("[ProjectionController] Sent GetProjectioEpsgCode request");
            return result;
        }

        [HttpPut("epsg")]
        public IActionResult SetProjectionEpsgCode([FromBody] int epsg)
        {
            var command = new SetProjectionEpsgCodeCommand(epsg, ref projectionLock);
            this.mediator.Send(command).Wait();
            Log.Information($"[ProjectionController] Sent SetProjectionEpsgCode request with code: {epsg}");
            return Ok();
        }
    }
}
