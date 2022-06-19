using System.IO;
using System.Linq;
using System.Reflection;

namespace DotaPlus.Core.Helpers
{
    public class EmbeddedResource
    {
        public static string ReadResource(string name)
        {
            using Stream stream = ReadStream(name);
            using StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }

        public static Stream ReadStream(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourcePath = assembly.GetManifestResourceNames().Single(str => str.EndsWith(name));
            return assembly.GetManifestResourceStream(resourcePath);
        }
    }
}
