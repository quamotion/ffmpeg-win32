using System.IO;
using System.Reflection;

namespace FFmpeg.Native
{
    public static class Binaries
    {
        public static string FindLibrary(string fileName, string[] relativePaths)
        {
            var assembly = typeof(MacOSBinaries).GetTypeInfo().Assembly;
            var assemblyLocation = assembly.Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

            foreach (var relativePath in relativePaths)
            {
                // Look for the library in the same location as this assembly. This will be the production
                // layout (i.e. the result of dotnet build, dotnet pack,...)
                var fullFileName = Path.Combine(assemblyDirectory, relativePath, fileName);

                if (File.Exists(fullFileName))
                {
                    return fullFileName;
                }
            }

            // Couldn't find the library.
            return null;
        }
    }
}