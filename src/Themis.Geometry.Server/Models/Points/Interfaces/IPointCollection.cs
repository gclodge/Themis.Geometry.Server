namespace Themis.Geometry.Server.Models.Points.Interfaces
{
    public interface IPointCollection
    {
        /// <summary>
        /// Count of all Point items contained within the IPointCollection
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Underlying collection of Point objects
        /// </summary>
        IEnumerable<Point> Points { get; }

        /// <summary>
        /// Add a single IPoint object to the PointCollection
        /// </summary>
        /// <param name="point">Point object to be added</param>
        /// <returns>Updated PointCollection object</returns>
        IPointCollection Add(Point point);
        /// <summary>
        /// Add all Points from an existing IPointCollection to this IPointCollection
        /// </summary>
        /// <param name="other">IPointCollection to be pulled from</param>
        /// <returns>Updated IPointCollection object</returns>
        IPointCollection Add(IPointCollection other);
        /// <summary>
        /// Add all Point object from an existing IEnumerable&lt;Point&gt; to this IPointCollection
        /// </summary>
        /// <param name="points">IEnumerable&lt;IPoint&gt; to be pulled from</param>
        /// <returns>Updated IPointCollection object</returns>
        IPointCollection Add(IEnumerable<Point> points);

        /// <summary>
        /// Parse incoming PointCollection JSON blob and then add all Points to this IPointCollection
        /// </summary>
        /// <param name="json">PointCollection JSON blob to parse</param>
        /// <returns>Updated IPointCollection object</returns>
        IPointCollection AddFromJson(string json);

        /// <summary>
        /// Serialize the current IPointCollection object into a JSON string
        /// </summary>
        /// <returns>JSON string representation of IPointCollection object</returns>
        string ToJson();
    }
}
