using Themis.Geometry.Server.Models.Points.Interfaces;

namespace Themis.Geometry.Server.Builders.Interfaces
{
    public interface IPointCollectionBuilder : IFluentBuilder<IPointCollection>
    {
        /// <summary>
        /// Add all available IPoint data from the input IPointCollection object
        /// </summary>
        /// <param name="pcoll">IPointCollection object to pull IPoints from</param>
        /// <returns>Updated IPointCollectionBuilder instance</returns>
        IPointCollectionBuilder WithPoints(IPointCollection pcoll);
        /// <summary>
        /// Add all available IPoint data from the input IEnumerable&lt;IPoint&gt;
        /// </summary>
        /// <param name="pcoll">IEnumerable&lt;IPoint&gt; object to pull IPoints from</param>
        /// <returns>Updated IPointCollectionBuilder instance</returns>
        IPointCollectionBuilder WithPoints(IEnumerable<IPoint> points);

        /// <summary>
        /// Parse incoming PointCollection JSON blob and add all IPoint records contained
        /// </summary>
        /// <param name="json">PointCollection JSON blob to parse</param>
        /// <returns>Updated IPointCollectionBuilder instance</returns>
        IPointCollectionBuilder AddFromJson(string json);
    }
}
