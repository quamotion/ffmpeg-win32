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
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return WindowsBinaries.FindFFmpegLibrary(name, version);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return MacOSBinaries.FindFFmpegLibrary(name, version);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return LinuxBinaries.FindFFmpegLibrary(name, version);
            }
            else
            {
                return null;
            }
        }
    }
}