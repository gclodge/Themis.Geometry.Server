namespace Themis.Geometry.Server.Services.Config
{
    public class ProjectionServiceConfig
    {
        public const string Name = "PROJECTION";

        /// <summary>
        /// Boolean flag indicating whether or not the underlying coordinate system is Spherical (geographic)
        /// </summary>
        public bool IS_GEOGRAPHIC { get; set; } = false;

        /// <summary>
        /// If available, the EPSG code for the underlying coordinate system
        /// </summary>
        public int? EPSG_CODE { get; set; }

        /// <summary>
        /// If available, the well-known-text (WKT) representation of underlying coordinate system
        /// NOTE: May be any WKT format (PROJ.4, ESRI WKT, OGC WKT, etc)
        /// </summary>
        public string? PROJECTION_WKT { get; set; }
    }
}
