using Microsoft.AspNetCore.Mvc;

using Themis.Geometry.Server.Models.Points;
using Themis.Geometry.Server.Models.Points.Interfaces;

using Themis.Geometry.Server.Commands;
using Themis.Geometry.Server.Services;

using MediatR;
using Serilog;

namespace Themis.Geometry.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        private object indexLock = PointProviderService.IndexLock;

        private readonly IMediator mediator;

        public PointsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("nearest")]   // GET /points
        public IPoint GetNearestNeighbour([FromBody] Point poi)
        {
            var command = new GetNearestNeighbourCommand(poi, ref indexLock);
            var result = this.mediator.Send(command).Result;
            Log.Information("[PointsController] Sent GetNearestNeighbour request for POI: {poi}", poi.Position.ToArray());
            return result;
        }

        [HttpGet("nearest/{count}")]   // GET /points/num
        public IEnumerable<IPoint> GetNearestNeighbour([FromBody] Point poi, int count)
        {
            var command = new GetNearestNeighboursCommand(poi, count, ref indexLock);
            var result = this.mediator.Send(command).Result;
            Log.Information("[PointsController] Sent GetNearestNeighbours request for POI: {poi}", poi.Position.ToArray());
            return result;
        }
    }
}
