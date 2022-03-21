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
    }
}
