using Themis.Geometry.Server.Models.Points.Interfaces;
using Themis.Geometry.Server.Services.Interfaces;

namespace Themis.Geometry.Server.Services
{
    public class FileSystemService : IFileSystemService
    {
        public const char Delim = '/';

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public string GetDirectoryName(string path)
        {
            if (DirectoryExists(path)) return new DirectoryInfo(path).Name;

            return path.Split(Delim).Last();
        }

        public IEnumerable<string> GetFiles(string path, string? searchPattern = null, bool allDirectories = false)
        {
            if (!DirectoryExists(path)) throw new DirectoryNotFoundException($"Unable to locate DIR: {path}");

            return Directory.GetFiles(path, searchPattern ?? "*", allDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }

        public IEnumerable<IPoint> LoadFromFile(string path)
        {
            //< Hit the PointFileParserFactory - returns an IPointFileParser
            //< Use that to return them points, add CSV/JSON to start

            throw new NotImplementedException();
        }
    }
}
