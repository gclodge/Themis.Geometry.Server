using System.IO;
using System.Linq;
using System.Collections.Generic;

using Bogus;
using Xunit;
using Assert = Xunit.Assert;

using Themis.Geometry.Server.Services;

namespace Themis.Geometry.Server.Tests.Services
{
    public class FileSystemServiceTests
    {
        Faker faker;
        FileSystemService ffs;

        public FileSystemServiceTests()
        {
            faker = new();
            ffs = new();
        }

        [Fact]
        public void CreateDirectoryTest()
        {
            var dirPath = Helper.GetTemporaryDirectoryName();
            var di = ffs.CreateDirectory(dirPath);

            Assert.True(di.Exists);

            Directory.Delete(dirPath);
        }

        [Fact]
        public void DirectoryExistsTest()
        {
            var goodPath = Path.GetTempPath();
            var badPath = Path.Combine(goodPath, string.Join('-', faker.Lorem.Words()));

            Assert.True(ffs.DirectoryExists(goodPath));
            Assert.False(ffs.DirectoryExists(badPath));
        }

        [Fact]
        public void FileExistsTest()
        {
            var goodFile = Path.GetTempFileName();
            var badFile = $"{goodFile}-{string.Join('-', faker.Lorem.Words())}";

            Assert.True(ffs.FileExists(goodFile));
            Assert.False(ffs.FileExists(badFile));
            
            File.Delete(goodFile);
        }

        [Fact]
        public void GetDirectoryNameTest()
        {
            var expected = faker.Random.Word();
            var path = Path.Combine(Path.GetTempPath(), expected);
            
            var actual = ffs.GetDirectoryName(path);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReadFileContentsTest()
        {
            var expected = faker.Lorem.Paragraph();
            var file = Path.GetTempFileName();

            File.WriteAllText(file, expected);

            var actual = ffs.ReadFileContents(file);

            Assert.Equal(expected, actual);

            File.Delete(file);
        }

        [Fact]
        public void GetAllFilesInDirectoryTest()
        {
            int count = 5;
            var dir = Helper.GetTemporaryDirectoryName();
            var expected = faker.GenerateRandomTextFiles(dir, count);

            ffs.CreateDirectory(dir);

            foreach (var file in expected) File.WriteAllText(file, faker.Lorem.Word());

            var actual = ffs.GetFiles(dir);

            Assert.Equal(expected.Count(), actual.Count());
            foreach (var file in actual) Assert.Contains(file, expected);
        }
    }
}
