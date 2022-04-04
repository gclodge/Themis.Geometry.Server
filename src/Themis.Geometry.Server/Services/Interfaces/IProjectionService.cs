using Themis.Geometry.Index.KdTree.TypeMath.Interfaces;

namespace Themis.Geometry.Server.Services.Interfaces
{
    public interface IProjectionService
    {
        /// <summary>
        /// Boolean flag indicating whether or not the underlying coordinate system is Spherical (geographic)
        /// </summary>
        bool IsGeographic { get; }
        /// <summary>
        /// Boolean flag indicating whether or not the underlying coordinate system is Euclidian (projected)
        /// </summary>
        bool IsProjected { get; }

        /// <summary>
        /// If available, the EPSG code for the underlying coordinate system
        /// </summary>
        int? EpsgCode { get; }

        /// <summary>
        /// If available, the well-known-text (WKT) representation of underlying coordinate system
        /// <para>NOTE: May be any WKT format (PROJ.4, ESRI WKT, OGC WKT, etc)</para>
        /// </summary>
        string? WellKnownText { get; }

        /// <summary>
        /// Get the ITypeMath&lt;double&gt; based on whether internal coordinate system is Geographic (spherical) or Euclidean (projected)
        /// </summary>
        /// <returns>ITypeMath&lt;double&gt; corresponding to internal coordinate system</returns>
        public ITypeMath<double> GetTypeMath();
    }
}
