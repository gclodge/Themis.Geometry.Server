using Themis.Geometry.Server.Models.Points.Interfaces;

namespace Themis.Geometry.Server.Services.Interfaces
{
    public interface IPointProviderService
    {
        public IPoint GetNearest(IPoint poi);

        public IEnumerable<IPoint> GetNearest(IPoint poi, int count);

        public IEnumerable<IPoint> GetAllWithin(IPoint point, double searchDistance);
    }
}
