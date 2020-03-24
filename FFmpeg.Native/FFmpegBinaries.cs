using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FFmpeg.Native
{
    public static class FFmpegBinaries
    {
        private static Dictionary<string, int> versions;

        static FFmpegBinaries()
        {
            versions = new Dictionary<string, int>();
            versions.Add("avcodec", 58);
            versions.Add("avdevice", 58);
            versions.Add("avfilter", 7);
            versions.Add("avformat", 58);
            versions.Add("avutil", 56);
            versions.Add("swresample", 3);
            versions.Add("swscale", 5);
        }

        public static string FindFFmpegLibrary(string name)
        {
            int version = versions[name];
            return FindFFmpegLibrary(name, version);
        }

        public static string FindFFmpegLibrary(string name, int version)
        {
            Binaries binaries = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                binaries = new WindowsBinaries();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                binaries = new MacOSBinaries();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                binaries = new LinuxBinaries();
            }
            else
            {
                return null;
            }

            return binaries.FindFFmpegLibrary(name, version);
        }
    }
}