namespace FFmpeg.Native
{
    public static class LinuxBinaries
    {
        public static string FindFFmpegLibrary(string name, int version)
        {
            var paths = new string[]
            {
                "../../runtimes/linux-x64/native/",
                "./runtimes/linux-x64/native/",
                "./",
            };

            var fileName = $"lib{name}.so.{version}";
            return Binaries.FindLibrary(fileName, paths);
        }
    }
}