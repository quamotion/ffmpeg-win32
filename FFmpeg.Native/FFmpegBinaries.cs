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
            versions.Add("avcodec", 57);
            versions.Add("avformat", 57);
            versions.Add("avutil", 55);
            versions.Add("swscale", 4);
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