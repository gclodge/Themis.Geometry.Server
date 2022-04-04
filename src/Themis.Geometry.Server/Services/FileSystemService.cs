using Themis.Geometry.Server.Services.Interfaces;

namespace Themis.Geometry.Server.Services
{
    public class FileSystemService : IFileSystemService
    {
        public const char Delim = '/';

        public DirectoryInfo CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }

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

        public string ReadFileContents(string path)
        {
            if (!FileExists(path)) throw new AppException($"Unable to locate FILE: {path}");

            return File.ReadAllText(path);
        }

        public IEnumerable<string> GetFiles(string path)
        {
            if (!DirectoryExists(path)) throw new AppException($"Unable to locate DIR: {path}");

            return Directory.GetFiles(path);
        }
    }
}
