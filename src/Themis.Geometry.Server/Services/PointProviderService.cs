using Themis.Geometry.Index.KdTree;
using Themis.Geometry.Index.KdTree.Interfaces;

using Themis.Geometry.Server.Builders;
using Themis.Geometry.Server.Models.Points;
using Themis.Geometry.Server.Models.Points.Interfaces;
using Themis.Geometry.Server.Services.Config;
using Themis.Geometry.Server.Services.Interfaces;

using Microsoft.Extensions.Options;

using Serilog;

namespace Themis.Geometry.Server.Services
{
    public class PointProviderService : IPointProviderService
    {
        public static readonly object IndexLock = new();

        private readonly PointProviderServiceConfig config;

        private readonly IKdTree<double, Point> index;
        private readonly IFileSystemService fileSystemService;
        private readonly IProjectionService projectionService;

        public int Count => index.Count;
        public int Dimensionality => index.Dimensions;

        public PointProviderService(IFileSystemService fss, IProjectionService projService, IOptions<PointProviderServiceConfig> cfg)
        {
            config = ParseConfiguration(cfg);
            
            fileSystemService = fss;
            projectionService = projService;

            index = new KdTree<double, Point>(config.DIMENSIONS, projectionService.GetTypeMath());
        }

        /// <summary>
        /// Attempt to parse the PointProviderServiceConfig as parsed from environment variable configuration
        /// </summary>
        /// <param name="opt">IOptions&lt;PointProviderServiceConfig&gt; containing available configuration</param>
        /// <returns>Fully composed PointProviderServiceConfig as configured</returns>
        static PointProviderServiceConfig ParseConfiguration(IOptions<PointProviderServiceConfig> opt)
        {
            PointProviderServiceConfig cfg;
            try
            {
                cfg = opt.Value;
                return cfg;
            }
            catch (OptionsValidationException ex)
            {
                foreach (var failure in ex.Failures) Log.Error(failure);
                throw;
            }
        }

        public IPointProviderService LoadExistingData()
        {
            var pcoll = LoadFromExistingFiles();

            return Add(pcoll.Points);
        }

        /// <summary>
        /// Loads any existing point data from configured files or directories on local disk
        /// </summary>
        /// <returns>IPointCollection containing all existing, available point data</returns>
        IPointCollection LoadFromExistingFiles()
        {
            var builder = new PointCollectionBuilder();

            if (config.POINT_DATA_FILE != null && fileSystemService.FileExists(config.POINT_DATA_FILE))
            {
                builder.AddPointsFromJson(fileSystemService.ReadFileContents(config.POINT_DATA_FILE));
            }

            return builder.Build();
        }

        public IPointProviderService Add(Point point)
        {
            index.Add(point.Position, point);
            return this;
        }

        public IPointProviderService Add(IEnumerable<Point> points)
        {
            foreach (var point in points) index.Add(point.Position, point);
            return this;
        }

        public IPointProviderService Remove(Point point)
        {
            index.Remove(point.Position);
            return this;
        }

        public IPointProviderService Remove(IEnumerable<Point> points)
        {
            foreach (var point in points) index.Remove(point.Position);
            return this;
        }

        public IEnumerable<Point> GetAllWithin(Point point, double searchDistance)
        {
            foreach (var node in index.RadialSearch(point.Position, searchDistance))
            {
                if (node.Value != null) yield return node.Value;
            }
        }

        public Point GetNearest(Point poi)
        {
            return GetNearest(poi, 1).SingleOrDefault() ?? new Point();
        }

        public IEnumerable<Point> GetNearest(Point poi, int count)
        {
            foreach (var node in index.GetNearestNeighbours(poi.Position, count))
            {
                if (node.Value != null) yield return node.Value;
            }
        }
    }
}
