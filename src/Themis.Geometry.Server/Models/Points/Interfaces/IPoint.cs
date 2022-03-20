namespace Themis.Geometry.Server.Models.Points.Interfaces
{
    public interface IPoint
    {
        double X { get; }
        double Y { get; }
        double Z { get; }

        IEnumerable<double> Position { get; }
        IDictionary<string, string> Attributes { get; }
    }
}
