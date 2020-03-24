using System;
using System.IO.Abstractions;

namespace FFmpeg.Native
{
    public class WindowsBinaries : Binaries
    {
        public WindowsBinaries()
            : base()
        {
        }

        public WindowsBinaries(IFileSystem fileSystem)
            : base(fileSystem)
        {
        }

        public override string FindFFmpegLibrary(string name, int version)
        {
            var runtime = "win7-x86";
            if (Environment.Is64BitProcess)
            {
                runtime = "win7-x64";
            }

            var paths = new string[]
            {
                this.FileSystem.Path.Combine("..","..","runtimes",$"{runtime}","native"),
                this.FileSystem.Path.Combine(".","runtimes",$"{runtime}","native"),
                ".",
            };

            var fileName = $"{name}-{version}.dll";
            return this.FindLibrary(fileName, paths);
        }
    }
}