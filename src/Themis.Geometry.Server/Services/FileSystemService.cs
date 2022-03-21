using Themis.Geometry.Server.Services.Interfaces;

using Serilog;

namespace Themis.Geometry.Server.Services
{
    public class FileSystemService : IFileSystemService
    {
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
            return new DirectoryInfo(path).Name;
        }

        public IEnumerable<string> GetFiles(string path, string? searchPattern = null, bool allDirectories = false)
        {
            return Directory.GetFiles(path, 
                        searchPattern ?? "*",
                        allDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }
    }
}
