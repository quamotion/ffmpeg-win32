using System.IO.Abstractions;
using System.Reflection;

namespace FFmpeg.Native
{
    public abstract class Binaries
    {
        public Binaries()
            : this(new FileSystem())
        {
        }

        public Binaries(IFileSystem fileSystem)
        {
            this.FileSystem = fileSystem;
        }

        internal IFileSystem FileSystem { get; set; }

        public abstract string FindFFmpegLibrary(string name, int version);

        internal string FindLibrary(string fileName, string[] relativePaths)
        {
            var assembly = typeof(Binaries).GetTypeInfo().Assembly;
            var assemblyLocation = assembly.Location;
            var assemblyDirectory = this.FileSystem.Path.GetDirectoryName(assemblyLocation);

            foreach (var relativePath in relativePaths)
            {
                var fullFileName = this.FileSystem.Path.Combine(assemblyDirectory, relativePath, fileName);

                if (this.FileSystem.File.Exists(fullFileName))
                {
                    return fullFileName;
                }
            }

            // Couldn't find the library.
            return null;
        }
    }
}