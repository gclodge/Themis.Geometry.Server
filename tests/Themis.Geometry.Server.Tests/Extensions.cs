using System.Linq;
using System.Collections.Generic;

using Bogus;

namespace Themis.Geometry.Server.Tests
{
    internal static class Extensions
    {
        internal static IEnumerable<double> GenerateRandomVector(this Faker f, int count)
        {
            return Enumerable.Range(0, count).Select(i => f.Random.Double()).ToArray();
        }

        internal static IDictionary<string, string> GenerateRandomDictionary(this Faker f, int count)
        {
            var dict = new Dictionary<string, string>();

            foreach (var key in f.GenerateUniqueWords(count)) { dict.Add(key, f.Random.Word()); }

            return dict;
        }

        internal static IEnumerable<string> GenerateUniqueWords(this Faker f, int count)
        {
            var words = new HashSet<string>();

            while (words.Count < count) { words.Add(f.Random.Word()); }

            return words;
        }
    }
}
