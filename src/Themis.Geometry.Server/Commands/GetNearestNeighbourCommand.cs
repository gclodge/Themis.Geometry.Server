using Themis.Geometry.Server.Models.Points.Interfaces;

using MediatR;

namespace Themis.Geometry.Server.Commands
{
    public class GetNearestNeighbourCommand : IRequest<IPoint>
    {
        /// <summary>
        /// The query Point-of-Interest (POI)
        /// </summary>
        public readonly IPoint POI;
        /// <summary>
        /// The lock mutex for the PointProviderService's internal index
        /// </summary>
        public readonly object IndexLock;

        public GetNearestNeighbourCommand(IPoint poi, ref object indexLock)
        {
            this.POI = poi;
            this.IndexLock = indexLock;
        }
    }
}
