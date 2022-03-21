using Themis.Geometry.Index.KdTree;
using Themis.Geometry.Index.KdTree.TypeMath;
using Themis.Geometry.Index.KdTree.Interfaces;

using Themis.Geometry.Server.Models.Points.Interfaces;
using Themis.Geometry.Server.Services.Interfaces;

using Microsoft.Extensions.Options;

using Serilog;

namespace Themis.Geometry.Server.Services
{
    public class PointProviderService : IPointProviderService
    {
        public static readonly object IndexLock = new object();

        private readonly PointProviderServiceConfig? config;
        private IKdTree<double, IPoint> index;

        public PointProviderService(IOptions<PointProviderServiceConfig> cfg)
        {
            config = ParseConfiguration(cfg);

            index = new KdTree<double, IPoint>(config.DIMENSIONS, new DoubleMath());

        }

        PointProviderServiceConfig ParseConfiguration(IOptions<PointProviderServiceConfig> opt)
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

        public IEnumerable<IPoint> GetAllWithin(IPoint point, double searchDistance)
        {
            throw new NotImplementedException();
        }

        public IPoint GetNearest(IPoint poi)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPoint> GetNearest(IPoint poi, int count)
        {
            throw new NotImplementedException();
        }
    }
}
