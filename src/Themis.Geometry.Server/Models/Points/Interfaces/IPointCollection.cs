namespace Themis.Geometry.Server.Models.Points.Interfaces
{
    public interface IPointCollection
    {
        /// <summary>
        /// Underlying collection of IPoint objects
        /// </summary>
        IEnumerable<IPoint> Points { get; }

        /// <summary>
        /// Add a single IPoint object to the IPointCollection
        /// </summary>
        /// <param name="point">IPoint object to be added</param>
        /// <returns>Updated IPointCollection object</returns>
        IPointCollection Add(IPoint point);
        /// <summary>
        /// Add all IPoints from an existing IPointCollection to this IPointCollection
        /// </summary>
        /// <param name="other">IPointCollection to be pulled from</param>
        /// <returns>Updated IPointCollection object</returns>
        IPointCollection Add(IPointCollection other);
        /// <summary>
        /// Add all IPoint object from an existing IEnumerable&lt;IPoint&gt; to this IPointCollection
        /// </summary>
        /// <param name="points">IEnumerable&lt;IPoint&gt; to be pulled from</param>
        /// <returns>Updated IPointCollection object</returns>
        IPointCollection Add(IEnumerable<IPoint> points);

        /// <summary>
        /// Parse incoming PointCollection JSON blob and then add all IPoints to this IPointCollection
        /// </summary>
        /// <param name="json">PointCollection JSON blob to parse</param>
        /// <returns>Updated IPointCollection object</returns>
        IPointCollection AddFromJson(string json);
    }
}
