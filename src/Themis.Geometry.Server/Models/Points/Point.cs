using Themis.Geometry.Server.Models.Points.Interfaces;

using Newtonsoft.Json;

namespace Themis.Geometry.Server.Models.Points
{
    public class Point : IPoint, IEquatable<Point>
    {
        public double X => Position.Count() > 0 ? Position.ElementAt(0) : double.NaN;
        public double Y => Position.Count() > 1 ? Position.ElementAt(1) : double.NaN;
        public double Z => Position.Count() > 2 ? Position.ElementAt(2) : double.NaN;

        [JsonProperty("position")]
        public IEnumerable<double> Position { get; set; } = new List<double>();

        [JsonProperty("attributes")]
        public IDictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();

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
    }
}
