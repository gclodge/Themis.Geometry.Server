using Themis.Geometry.Server.Commands;
using Themis.Geometry.Server.Services.Interfaces;

using Themis.Geometry.Server.Models.Points;

using MediatR;

namespace Themis.Geometry.Server.Handlers
{
    public class GetNearestNeighboursHandler : IRequestHandler<GetNearestNeighboursCommand, IEnumerable<Point>>
    {
        readonly IPointProviderService pointProviderService;

        public GetNearestNeighboursHandler(IPointProviderService pointService)
        {
            this.pointProviderService = pointService;
        }

        public Task<IEnumerable<Point>> Handle(GetNearestNeighboursCommand request, CancellationToken cancellationToken)
        {
            lock (request.IndexLock)
            {
                if (request.Count <= 0) return Task.FromResult(new List<Point>() as IEnumerable<Point>);

                var nearest = pointProviderService.GetNearest(request.POI, request.Count);
                return Task.FromResult(nearest);
            }
        }
    }
}
