using System.IO;

namespace Themis.Geometry.Server.Tests
{
    internal static class Helper
    {
        internal static string GetTemporaryDirectoryName()
        {
            return Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetTempFileName()));
        }
    }
}
