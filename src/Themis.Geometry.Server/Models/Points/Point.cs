using Themis.Geometry.Server.Models.Points.Interfaces;

using Newtonsoft.Json;

namespace Themis.Geometry.Server.Models.Points
{
    public class Point : IPoint, IEquatable<Point>
    {
        public const int MinimumDimensionality = 2;
        public const int MaximumDimensionality = 1337; //< comedy.init();

        public double X => Position.ElementAt(0);
        public double Y => Position.ElementAt(1);
        public double Z => Position.Count() > 2 ? Position.ElementAt(2) : double.NaN;

        [JsonProperty("position")]
        public IEnumerable<double> Position { get; set; } = new List<double>();

        [JsonProperty("attributes")]
        public IDictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();

        #region Fluent Interface
        public static Point FromPoint(IPoint other)
        {
            return new Point()
            {
                Position = other.Position,
                Attributes = other.Attributes
            };
        }

        public Point WithPosition(IPoint other)
        {
            return WithPosition(other.Position);
        }

        public Point WithPosition(IEnumerable<double> pos)
        {
            this.Position = pos.ToList();
            return this;
        }

        public Point WithAttributes(IPoint other)
        {
            return WithAttributes(other.Attributes);
        }

        public Point WithAttributes(IDictionary<string, string> atts)
        {
            this.Attributes = atts;
            return this;
        }
        #endregion

        #region IEquatable Members
        public override bool Equals(object? obj)
        {
            return Equals(obj as Point);
        }

        public bool Equals(Point? other)
        {
            return other != null
                && Position.SequenceEqual(other.Position)
                && Attributes.SequenceEqual(other.Attributes);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Attributes);
        }
        #endregion
    }
}
