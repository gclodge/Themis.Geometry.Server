using Themis.Geometry.Index.KdTree.Interfaces;

using Themis.Geometry.Server.Models.Points;

namespace Themis.Geometry.Server.Services.Interfaces
{
    public interface IPointProviderService
    {
        /// <summary>
        /// The total numbers of elements contained in the underlying K-D Tree point index
        /// </summary>
        int Count { get; }
        /// <summary>
        /// The dimensionality (K) of the underlying K-D Tree point index
        /// </summary>
        int Dimensionality { get; }

        /// <summary>
        /// Loads any existing Point data into the point index from configured source path(s)
        /// </summary>
        /// <returns>Current state of internal IKdTree&lt;double, Point&gt; point index</returns>
        public IPointProviderService LoadExistingData();

        /// <summary>
        /// Adds a given Point to the underlying point index
        /// </summary>
        /// <param name="point">Point to be added</param>
        /// <returns>Current state of internal IKdTree&lt;double, Point&gt; point index</returns>
        public IPointProviderService Add(Point point);
        /// <summary>
        /// Adds all Points from input IEnumerable&lt;Point&gt; to the underlying point index
        /// </summary>
        /// <param name="points">IEnumerable&lt;Point&gt; containing points to be added</param>
        /// <returns>Current state of internal IKdTree&lt;double, Point&gt; point index</returns>
        public IPointProviderService Add(IEnumerable<Point> points);
        /// <summary>
        /// Attempts to remove the input Point from the underlying point index
        /// </summary>
        /// <param name="point"></param>
        /// <returns>Current state of internal IKdTree&lt;double, Point&gt; point index</returns>
        public IPointProviderService Remove(Point point);
        /// <summary>
        /// Attempts to remove each Point in the input IEnumerable&lt;Point&gt; from the underlying point index
        /// </summary>
        /// <param name="points">IEnumerable&lt;Point&gt; containing points to be removed</param>
        /// <returns>Current state of internal IKdTree&lt;double, Point&gt; point index</returns>
        public IPointProviderService Remove(IEnumerable<Point> points);

        /// <summary>
        /// Finds and returns the nearest other Point to the input point-of-interest (POI)
        /// </summary>
        /// <param name="poi">Query point-of-interest</param>
        /// <returns>Nearest Point</returns>
        public Point GetNearest(Point poi);

        /// <summary>
        /// Finds and returns the N nearest other Points to the input point-of-interest (POI)
        /// </summary>
        /// <param name="poi">Query point-of-interest</param>
        /// <param name="count">Count (N) of nearest neighbours to grab</param>
        /// <returns>Nearest N Points</returns>
        public IEnumerable<Point> GetNearest(Point poi, int count);

        /// <summary>
        /// FInd and return all other Point records within a specified radial search distance
        /// </summary>
        /// <param name="point">Query point-of-interest</param>
        /// <param name="searchDistance">Radial search distance (in source units)</param>
        /// <returns>All Points w/in the search distance</returns>
        public IEnumerable<Point> GetAllWithin(Point point, double searchDistance);
    }
}
