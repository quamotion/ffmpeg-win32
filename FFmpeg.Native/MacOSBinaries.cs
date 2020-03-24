using System.IO.Abstractions;

namespace FFmpeg.Native
{
    public class MacOSBinaries: Binaries
    {
        public MacOSBinaries()
            : base()
        {
        }

        public MacOSBinaries(IFileSystem fileSystem)
            : base(fileSystem)
        {
        }

        public override string FindFFmpegLibrary(string name, int version)
        {
            var paths = new string[]
            {
                this.FileSystem.Path.Combine("..","..","runtimes","osx-x64","native"),
                this.FileSystem.Path.Combine(".","runtimes","osx-x64","native"),
                ".",
            };

            var fileName = $"lib{name}.{version}.dylib";
            return this.FindLibrary(fileName, paths);
        }
    }
}