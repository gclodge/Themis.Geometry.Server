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
        /// Create a new directory at the given path
        /// </summary>
        /// <param name="path">Path of directory to be created</param>
        /// <returns>True if successful</returns>
        DirectoryInfo CreateDirectory(string path);

        /// <summary>
        /// Get the directory name (last part) of the given path
        /// </summary>
        /// <param name="path">Directory path to analyze</param>
        /// <returns>The name of the given directory path</returns>
        string GetDirectoryName(string path);
        /// <summary>
        /// Read all contents of a given filepath into memory as a single string
        /// </summary>
        /// <param name="path">File path to be parsed</param>
        /// <returns>Single string of all available file contents</returns>
        string ReadFileContents(string path);

        /// <summary>
        /// Gets and returns all files within the given directory path
        /// </summary>
        /// <param name="path">Directory path to be enumerated</param>
        /// <returns>IEnumerable&lt;string&gt; of all files within input directory</returns>
        IEnumerable<string> GetFiles(string path);
    }
}
