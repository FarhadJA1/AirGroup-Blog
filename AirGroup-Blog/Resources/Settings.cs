using System.IO;
using System.Reflection;

namespace AirGroup_Blog.Resources
{
    public class Settings
    {
        public readonly string FilePath;

        public Settings()
        {
            var assemblyFolder = Assembly.GetExecutingAssembly().Location;

            var path = Path.Combine(Path.GetDirectoryName(assemblyFolder)!, "Data.txt");

            FilePath = path;
        }


    }
}
