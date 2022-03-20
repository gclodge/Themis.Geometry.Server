using Themis.Geometry.Server.Models.Points.Interfaces;

using MediatR;

namespace Themis.Geometry.Server.Commands
{
    public class GetNearestNeighboursCommand : IRequest<IEnumerable<IPoint>>
    {
        /// <summary>
        /// The maximum number of nearest-neighbours to search for
        /// </summary>
        public readonly int Count;
        /// <summary>
        /// The query Point-of-Interest (POI)
        /// </summary>
        public readonly IPoint POI;
        /// <summary>
        /// The lock mutex for the PointProviderService's internal index
        /// </summary>
        public readonly object IndexLock;

        public GetNearestNeighboursCommand(IPoint poi, int count, ref object indexLock)
        {
            this.Count = count;
            this.POI = poi;
            this.IndexLock = indexLock;
        }
    }
}
