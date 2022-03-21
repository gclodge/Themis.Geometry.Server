using Themis.Geometry.Server.Models.Points.Interfaces;

using Serilog;
using Newtonsoft.Json;

namespace Themis.Geometry.Server.Models.Points
{
    public class PointCollection : IPointCollection
    {
        public IEnumerable<IPoint> Points { get; set; } = new List<IPoint>();

        #region IPointCollection members
        public IPointCollection Add(IPoint point)
        {
            _ = Points.Append(point);
            return this;
        }

        public IPointCollection Add(IPointCollection other)
        {
            return Add(other.Points);
        }

        public IPointCollection Add(IEnumerable<IPoint> points)
        {
            _ = Points.Concat(points);
            return this;
        }

        public IPointCollection AddFromJson(string json)
        {
            PointCollection? pcoll = null;
            try
            {
                pcoll = JsonConvert.DeserializeObject<PointCollection>(json);
            }
            catch (Exception ex) 
            { 
                Log.Error(ex, "Failed to parse IPointCollection from JSON: {json}", json);
            }

            if (pcoll != null) return Add(pcoll);

            return this;
        }
        #endregion
    }
}
