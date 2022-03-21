using Themis.Geometry.Server.Builders.Interfaces;
using Themis.Geometry.Server.Models.Points;
using Themis.Geometry.Server.Models.Points.Interfaces;

namespace Themis.Geometry.Server.Builders
{
    public class PointCollectionBuilder : IPointCollectionBuilder
    {
        private PointCollection points = new();

        #region IPointCollectionBuilder members
        public IPointCollectionBuilder AddFromJson(string json)
        {
            this.points.AddFromJson(json);
            return this;
        }

        public IPointCollectionBuilder WithPoints(IPointCollection pcoll)
        {
            return WithPoints(pcoll.Points);
        }

        public IPointCollectionBuilder WithPoints(IEnumerable<IPoint> points)
        {
            this.points.Add(points);
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
