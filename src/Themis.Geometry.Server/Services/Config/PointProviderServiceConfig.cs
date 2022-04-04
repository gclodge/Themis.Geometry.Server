using System.ComponentModel.DataAnnotations;

using Themis.Geometry.Server.Models.Points;

namespace Themis.Geometry.Server.Services.Config
{
    public class PointProviderServiceConfig
    {
        public const string Name = "POINTS";

        /// <summary>
        /// The dimensionality of the underlying K-D Tree index of the IPoint data, minimum is 2.
        /// </summary>
        [Range(Point.MinimumDimensionality, Point.MaximumDimensionality, ErrorMessage = "Value for {0} must be between [{1}, {2}].")]
        public int DIMENSIONS { get; set; } = Point.MinimumDimensionality;

        /// <summary>
        /// File path or directory of existing point data to be pre-loaded at server standup
        /// </summary>
        public string? POINT_DATA_FILE { get; set; }
    }
}
