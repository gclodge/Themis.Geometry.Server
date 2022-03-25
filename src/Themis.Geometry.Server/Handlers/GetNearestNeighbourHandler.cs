using Themis.Geometry.Server.Commands;
using Themis.Geometry.Server.Services.Interfaces;

using Themis.Geometry.Server.Models.Points;

using MediatR;

namespace Themis.Geometry.Server.Handlers
{
    public class GetNearestNeighbourHandler : IRequestHandler<GetNearestNeighbourCommand, Point>
    {
        readonly IPointProviderService pointProviderService;

        public GetNearestNeighbourHandler(IPointProviderService pointService)
        {
            this.pointProviderService = pointService;
        }

        public Task<Point> Handle(GetNearestNeighbourCommand request, CancellationToken cancellationToken)
        {
            lock (request.IndexLock)
            {
                var nearest = pointProviderService.GetNearest(request.POI);
                return Task.FromResult(nearest ?? new Point());
            }
        }
    }
}
