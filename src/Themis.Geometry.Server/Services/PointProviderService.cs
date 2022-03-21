using Themis.Geometry.Index.KdTree;
using Themis.Geometry.Index.KdTree.TypeMath;
using Themis.Geometry.Index.KdTree.Interfaces;

using Themis.Geometry.Server.Models.Points;
using Themis.Geometry.Server.Models.Points.Interfaces;
using Themis.Geometry.Server.Services.Interfaces;

using Microsoft.Extensions.Options;

using Serilog;

namespace Themis.Geometry.Server.Services
{
    public class PointProviderService : IPointProviderService
    {
        public static readonly object IndexLock = new object();

        private readonly PointProviderServiceConfig config;

        private IKdTree<double, IPoint> index;
        private readonly IFileSystemService fss;

        public PointProviderService(IFileSystemService fileSystemService, IOptions<PointProviderServiceConfig> cfg)
        {
            config = ParseConfiguration(cfg);
            index = new KdTree<double, IPoint>(config.DIMENSIONS, new DoubleMath());
            fss = fileSystemService;
        }

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

        public IKdTree<double, IPoint> LoadExistingData()
        {
            if (config.POINT_DATA_FILE != null && fss.FileExists(config.POINT_DATA_FILE))
            {
                return LoadFromFile(config.POINT_DATA_FILE);
            }
            return index;
        }

        IKdTree<double, IPoint> LoadFromFile(string pointFile)
        {
            //foreach (var point in fss.LoadFromFile(pointFile)) index.Add(point.Position, point);

            //return index;

            throw new NotImplementedException();
        }

        public IEnumerable<IPoint> GetAllWithin(IPoint point, double searchDistance)
        {
            foreach (var node in index.RadialSearch(point.Position, searchDistance))
            {
                if (node.Value != null) yield return node.Value;
            }
        }

        public IPoint GetNearest(IPoint poi)
        {
            return GetNearest(poi, 1).SingleOrDefault() ?? new Point();
        }

        public IEnumerable<IPoint> GetNearest(IPoint poi, int count)
        {
            foreach (var node in index.GetNearestNeighbours(poi.Position, count))
            {
                if (node.Value != null) yield return node.Value;
            }
        }
    }
}
