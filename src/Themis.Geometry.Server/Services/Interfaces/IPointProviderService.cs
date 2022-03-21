using Themis.Geometry.Index.KdTree.Interfaces;

using Themis.Geometry.Server.Models.Points.Interfaces;

namespace Themis.Geometry.Server.Services.Interfaces
{
    public interface IPointProviderService
    {
        /// <summary>
        /// Loads any existing IPoint data into the point index from configured source path(s)
        /// </summary>
        /// <returns>Current state of internal IKdTree&lt;double, IPoint&gt; point index</returns>
        public IKdTree<double, IPoint> LoadExistingData();

        /// <summary>
        /// Finds and returns the nearest other IPoint to the input point-of-interest (POI)
        /// </summary>
        /// <param name="poi">Query point-of-interest</param>
        /// <returns>Nearest IPoint</returns>
        public IPoint GetNearest(IPoint poi);

        /// <summary>
        /// Finds and returns the N nearest other IPoints to the input point-of-interest (POI)
        /// </summary>
        /// <param name="poi">Query point-of-interest</param>
        /// <param name="count">Count (N) of nearest neighbours to grab</param>
        /// <returns>Nearest N IPoints</returns>
        public IEnumerable<IPoint> GetNearest(IPoint poi, int count);

        /// <summary>
        /// FInd and return all other IPoint records within a specified radial search distance
        /// </summary>
        /// <param name="point">Query point-of-interest</param>
        /// <param name="searchDistance">Radial search distance (in source units)</param>
        /// <returns>All IPoints w/in the search distance</returns>
        public IEnumerable<IPoint> GetAllWithin(IPoint point, double searchDistance);
    }
}
