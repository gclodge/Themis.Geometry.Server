using Themis.Geometry.Server.Models.Points.Interfaces;
using Themis.Geometry.Server.Services.Interfaces;

namespace Themis.Geometry.Server.Services
{
    public class PointProviderService : IPointProviderService
    {
        public static readonly object IndexLock = new object();

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
