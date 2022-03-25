using Themis.Geometry.Server.Models.Points;

using MediatR;

namespace Themis.Geometry.Server.Commands
{
    public class GetNearestNeighbourCommand : IRequest<Point>
    {
        /// <summary>
        /// The query Point-of-Interest (POI)
        /// </summary>
        public readonly Point POI;
        /// <summary>
        /// The lock mutex for the PointProviderService's internal index
        /// </summary>
        public readonly object IndexLock;

        public GetNearestNeighbourCommand(Point poi, ref object indexLock)
        {
            this.POI = poi;
            this.IndexLock = indexLock;
        }
    }
}
