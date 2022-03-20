using Themis.Geometry.Server.Commands;
using Themis.Geometry.Server.Services.Interfaces;

using Themis.Geometry.Server.Models.Points;
using Themis.Geometry.Server.Models.Points.Interfaces;

using MediatR;

namespace Themis.Geometry.Server.Handlers
{
    public class GetNearestNeighboursHandler : IRequestHandler<GetNearestNeighboursCommand, IEnumerable<IPoint>>
    {
        readonly IPointProviderService pointProviderService;

        public GetNearestNeighboursHandler(IPointProviderService pointService)
        {
            this.pointProviderService = pointService;
        }

        public Task<IEnumerable<IPoint>> Handle(GetNearestNeighboursCommand request, CancellationToken cancellationToken)
        {
            lock (request.IndexLock)
            {
                if (request.Count <= 0) return Task.FromResult(new List<IPoint>() as IEnumerable<IPoint>);

                var nearest = pointProviderService.GetNearest(request.POI, request.Count);
                return Task.FromResult(nearest);
            }
        }
    }
}
