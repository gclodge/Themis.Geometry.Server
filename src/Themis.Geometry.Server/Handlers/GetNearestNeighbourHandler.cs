using Themis.Geometry.Server.Commands;
using Themis.Geometry.Server.Services.Interfaces;

using Themis.Geometry.Server.Models.Points;
using Themis.Geometry.Server.Models.Points.Interfaces;

using MediatR;

namespace Themis.Geometry.Server.Handlers
{
    public class GetNearestNeighbourHandler : IRequestHandler<GetNearestNeighbourCommand, IPoint>
    {
        readonly IPointProviderService pointProviderService;

        public GetNearestNeighbourHandler(IPointProviderService pointService)
        {
            this.pointProviderService = pointService;
        }

        public Task<IPoint> Handle(GetNearestNeighbourCommand request, CancellationToken cancellationToken)
        {
            lock (request.IndexLock)
            {
                var nearest = pointProviderService.GetNearest(request.POI);
                return Task.FromResult(nearest ?? new Point());
            }
        }
    }
}
