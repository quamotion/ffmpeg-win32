using System;
using System.IO;
using System.Reflection;

namespace FFmpeg.Native
{
    public static class WindowsBinaries
    {
        public static string FindFFmpegLibrary(string name, int version)
        {
            var assembly = typeof(WindowsBinaries).GetTypeInfo().Assembly;
            var assemblyLocation = assembly.Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

            var fileName = $"{name}-{version}.dll";

            // Look for the library in the same location as this assembly. This will be the production
            // layout (i.e. the result of dotnet build, dotnet pack,...)
            var fullFileName = Path.Combine(assemblyDirectory, fileName);

            if (File.Exists(fullFileName))
            {
                return fullFileName;
            }

            // Alternatively, try the "runtimes" directory. This is the layout when this assembly
            // is loaded from a NuGet package (i.e. unit testing,...)
            var nativeDirectory = Path.Combine(assemblyDirectory, "../../runtimes");
            
            if (Environment.Is64BitProcess)
            {
                nativeDirectory = Path.Combine(nativeDirectory, "win7-x64");
            }
            else
            {
                nativeDirectory = Path.Combine(nativeDirectory, "win7-x86");
            }
            
            nativeDirectory = Path.Combine(nativeDirectory, "native");
            
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