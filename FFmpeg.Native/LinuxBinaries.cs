using System.IO;
using System.Reflection;

namespace FFmpeg.Native
{
    public static class LinuxBinaries
    {
        public static string FindFFmpegLibrary(string name, int version)
        {
            var assembly = typeof(LinuxBinaries).GetTypeInfo().Assembly;
            var assemblyLocation = assembly.Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

            var fileName = $"lib{name}.so.{version}";

            // Look for the library in the same location as this assembly. This will be the production
            // layout (i.e. the result of dotnet build, dotnet pack,...)
            var fullFileName = Path.Combine(assemblyDirectory, fileName);

            if (File.Exists(fullFileName))
            {
                return fullFileName;
            }

            // Alternatively, try the "runtimes" directory. This is the layout when this assembly
            // is loaded from a NuGet package (i.e. unit testing,...)
            var nativeDirectory = Path.Combine(assemblyDirectory, "../../runtimes/linux-x64/native/");
            fullFileName = Path.Combine(nativeDirectory, fileName);

            if (File.Exists(fullFileName))
            {
                return fullFileName;
            }

            // Couldn't find the library.
            return null;
        }
    }
}