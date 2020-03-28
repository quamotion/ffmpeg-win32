using System.IO.Abstractions;

namespace FFmpeg.Native
{
    public class LinuxBinaries: Binaries
    {
        public LinuxBinaries()
            : base()
        {
        }

        public LinuxBinaries(IFileSystem fileSystem)
            : base(fileSystem)
        {
        }

        public override string FindFFmpegLibrary(string name, int version)
        {
            var paths = new string[]
            {
                this.FileSystem.Path.Combine("..","..","runtimes","linux-x64","native"),
                this.FileSystem.Path.Combine(".","runtimes","linux-x64","native"),
                ".",
            };

            var fileName = $"lib{name}.so.{version}";
            return this.FindLibrary(fileName, paths);
        }
    }
}