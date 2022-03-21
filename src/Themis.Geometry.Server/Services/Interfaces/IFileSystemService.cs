using Themis.Geometry.Server.Models.Points.Interfaces;

namespace Themis.Geometry.Server.Services.Interfaces
{
    public interface IFileSystemService
    {
        /// <summary>
        /// Check if a file exists at the given path
        /// </summary>
        /// <param name="path">File path to check</param>
        /// <returns>True if a file is found at the given path</returns>
        bool FileExists(string path);
        /// <summary>
        /// Check if a directory exists at the given path
        /// </summary>
        /// <param name="path">Directory path to check</param>
        /// <returns>True if a directory is found at the given path</returns>
        bool DirectoryExists(string path);

        /// <summary>
        /// Get the directory name (last part) of the given path
        /// </summary>
        /// <param name="path">Directory path to analyze</param>
        /// <returns>The name of the given directory path</returns>
        string GetDirectoryName(string path);

        /// <summary>
        /// Find and return files at the given path based on an optional search pattern
        /// </summary>
        /// <param name="path">Directory path to search</param>
        /// <param name="searchPattern">Search pattern to be applied for file selection</param>
        /// <param name="allDirectories">Flag to indicate if we should search all sub-directories as well</param>
        /// <returns></returns>
        IEnumerable<string> GetFiles(string path, string? searchPattern = null, bool allDirectories = false);

        /// <summary>
        /// Loads all existing IPoint objects from a given file path
        /// </summary>
        /// <param name="path">File to be parsed</param>
        /// <returns>ALl available IPoint object records</returns>
        IEnumerable<IPoint> LoadFromFile(string path);
    }
}
