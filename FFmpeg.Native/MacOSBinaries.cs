namespace FFmpeg.Native
{
    public static class MacOSBinaries
    {
        public static string FindFFmpegLibrary(string name, int version)
        {
            var paths = new string[]
            {
                "../../runtimes/osx-x64/native/",
                "./runtimes/osx-x64/native/",
                "./",
            };

            var fileName = $"lib{name}.{version}.dylib";
            return Binaries.FindLibrary(fileName, paths);
        }
    }
}