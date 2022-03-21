using Themis.Geometry.Server.Builders.Interfaces;
using Themis.Geometry.Server.Models.Points;
using Themis.Geometry.Server.Models.Points.Interfaces;

namespace Themis.Geometry.Server.Builders
{
    public class PointCollectionBuilder : IPointCollectionBuilder
    {
        private PointCollection points = new();

        #region IPointCollectionBuilder members
        public IPointCollectionBuilder AddPoint(IPoint point)
        {
            this.points.Add(point);
            return this;
        }

        public IPointCollectionBuilder AddPoints(IPointCollection pcoll)
        {
            return AddPoints(pcoll.Points);
        }

        public IPointCollectionBuilder AddPoints(IEnumerable<IPoint> points)
        {
            this.points.Add(points);
            return this;
        }

        public IPointCollectionBuilder AddPointsFromJson(string json)
        {
            this.points.AddFromJson(json);
            return this;
        }
        #endregion

        #region IFluentBuilder<PointCollection> members
        public IPointCollection Build(bool reset = true)
        {
            PointCollection result = points;

            if (reset) Reset();

            return result;
        }

        public void Reset()
        {
            points = new();
        }
        #endregion
    }
}
