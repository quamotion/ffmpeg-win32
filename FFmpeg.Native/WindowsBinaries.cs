using System;

namespace FFmpeg.Native
{
    public static class WindowsBinaries
    {
        public static string FindFFmpegLibrary(string name, int version)
        {
            var runtime = "win7-x86";
            if (Environment.Is64BitProcess)
            {
                runtime = "win7-x64";
            }

            var paths = new string[]
            {
                $"../../runtimes/{runtime}/native/",
                $"./runtimes/{runtime}/native/",
                "./",
            };

            var fileName = $"{name}-{version}.dll";
            return Binaries.FindLibrary(fileName, paths);
        }
    }
}