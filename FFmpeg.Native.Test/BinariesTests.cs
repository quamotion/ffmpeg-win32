using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Reflection;
using Xunit;

namespace FFmpeg.Native.Test
{
    public class BinariesTests
    {
        private readonly string assemblyDirectory;

        public BinariesTests()
        {
            var assembly = typeof(Binaries).GetTypeInfo().Assembly;
            var assemblyLocation = assembly.Location;
            assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
        }

        [Fact]
        public void FindWindowsBinaries1()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { Path.Combine(assemblyDirectory,"..","..","runtimes","win7-x86","native","avutil-56.dll"), new MockFileData("avutil-56.dll") },
                { Path.Combine(assemblyDirectory,"..","..","runtimes","win7-x64","native","avutil-56.dll"), new MockFileData("avutil-56.dll") },
                { Path.Combine(assemblyDirectory,"..","..","runtimes","linux-x64","native","libavutil.so.56"), new MockFileData("libavutil.so.56") },
                { Path.Combine(assemblyDirectory,"..","..","runtimes","osx-x64","native","libavutil.56.dylib"), new MockFileData("libavutil.56.dylib") }
            });

            var runtime = "win7-x86";
            if (Environment.Is64BitProcess)
            {
                runtime = "win7-x64";
            }

            var windowsBinaries = new WindowsBinaries(fileSystem);

            Assert.Equal(Path.GetFullPath(Path.Combine(assemblyDirectory, "..", "..", "runtimes", runtime, "native", "avutil-56.dll")), Path.GetFullPath(windowsBinaries.FindFFmpegLibrary("avutil", 56)));
        }

        [Fact]
        public void FindWindowsBinaries2()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { Path.Combine(assemblyDirectory,"runtimes","win7-x86","native","avutil-56.dll"), new MockFileData("avutil-56.dll") },
                { Path.Combine(assemblyDirectory,"runtimes","win7-x64","native","avutil-56.dll"), new MockFileData("avutil-56.dll") },
                { Path.Combine(assemblyDirectory,"runtimes","linux-x64","native","libavutil.so.56"), new MockFileData("libavutil.so.56") },
                { Path.Combine(assemblyDirectory,"runtimes","osx-x64","native","libavutil.56.dylib"), new MockFileData("libavutil.56.dylib") }
            });

            var runtime = "win7-x86";
            if (Environment.Is64BitProcess)
            {
                runtime = "win7-x64";
            }

            var windowsBinaries = new WindowsBinaries(fileSystem);

            Assert.Equal(Path.GetFullPath(Path.Combine(assemblyDirectory, "runtimes", runtime, "native", "avutil-56.dll")), Path.GetFullPath(windowsBinaries.FindFFmpegLibrary("avutil", 56)));
        }

        [Fact]
        public void FindWindowsBinaries3()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { Path.Combine(assemblyDirectory,"avutil-56.dll"), new MockFileData("avutil-56.dll") },
                { Path.Combine(assemblyDirectory,"libavutil.so.56"), new MockFileData("libavutil.so.56") },
                { Path.Combine(assemblyDirectory,"libavutil.56.dylib"), new MockFileData("libavutil.56.dylib") }
            });

            var windowsBinaries = new WindowsBinaries(fileSystem);

            Assert.Equal(Path.GetFullPath(Path.Combine(assemblyDirectory, "avutil-56.dll")), Path.GetFullPath(windowsBinaries.FindFFmpegLibrary("avutil", 56)));
        }

        [Fact]
        public void FindLinuxBinaries1()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { Path.Combine(assemblyDirectory,"..","..","runtimes","win7-x86","native","avutil-56.dll"), new MockFileData("avutil-56.dll") },
                { Path.Combine(assemblyDirectory,"..","..","runtimes","win7-x64","native","avutil-56.dll"), new MockFileData("avutil-56.dll") },
                { Path.Combine(assemblyDirectory,"..","..","runtimes","linux-x64","native","libavutil.so.56"), new MockFileData("libavutil.so.56") },
                { Path.Combine(assemblyDirectory,"..","..","runtimes","osx-x64","native","libavutil.56.dylib"), new MockFileData("libavutil.56.dylib") }
            });

            var linuxBinaries = new LinuxBinaries(fileSystem);

            Assert.Equal(Path.GetFullPath(Path.Combine("..", "..", "runtimes", "linux-x64", "native", "libavutil.so.56")), Path.GetFullPath(linuxBinaries.FindFFmpegLibrary("avutil", 56)));
        }

        [Fact]
        public void FindLinuxBinaries2()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { Path.Combine(assemblyDirectory,"runtimes","win7-x86","native","avutil-56.dll"), new MockFileData("avutil-56.dll") },
                { Path.Combine(assemblyDirectory,"runtimes","win7-x64","native","avutil-56.dll"), new MockFileData("avutil-56.dll") },
                { Path.Combine(assemblyDirectory,"runtimes","linux-x64","native","libavutil.so.56"), new MockFileData("libavutil.so.56") },
                { Path.Combine(assemblyDirectory,"runtimes","osx-x64","native","libavutil.56.dylib"), new MockFileData("libavutil.56.dylib") }
            });

            var linuxBinaries = new LinuxBinaries(fileSystem);

            Assert.Equal(Path.GetFullPath(Path.Combine(assemblyDirectory, "runtimes", "linux-x64", "native", "libavutil.so.56")), Path.GetFullPath(linuxBinaries.FindFFmpegLibrary("avutil", 56)));
        }

        [Fact]
        public void FindLinuxBinaries3()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { Path.Combine(assemblyDirectory,"avutil-56.dll"), new MockFileData("avutil-56.dll") },
                { Path.Combine(assemblyDirectory,"libavutil.so.56"), new MockFileData("libavutil.so.56") },
                { Path.Combine(assemblyDirectory,"libavutil.56.dylib"), new MockFileData("libavutil.56.dylib") }
            });

            var linuxBinaries = new LinuxBinaries(fileSystem);

            Assert.Equal(Path.GetFullPath(Path.Combine(assemblyDirectory, "libavutil.so.56")), Path.GetFullPath(linuxBinaries.FindFFmpegLibrary("avutil", 56)));
        }

        [Fact]
        public void FindMacOSBinaries1()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { Path.Combine(assemblyDirectory,"..","..","runtimes","win7-x86","native","avutil-56.dll"), new MockFileData("avutil-56.dll") },
                { Path.Combine(assemblyDirectory,"..","..","runtimes","win7-x64","native","avutil-56.dll"), new MockFileData("avutil-56.dll") },
                { Path.Combine(assemblyDirectory,"..","..","runtimes","linux-x64","native","libavutil.so.56"), new MockFileData("libavutil.so.56") },
                { Path.Combine(assemblyDirectory,"..","..","runtimes","osx-x64","native","libavutil.56.dylib"), new MockFileData("libavutil.56.dylib") }
            });

            var macOSBinaries = new MacOSBinaries(fileSystem);

            Assert.Equal(Path.GetFullPath(Path.Combine("..", "..", "runtimes", "osx-x64", "native", "libavutil.56.dylib")), Path.GetFullPath(macOSBinaries.FindFFmpegLibrary("avutil", 56)));
        }

        [Fact]
        public void FindMMacOSBinaries2()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { Path.Combine(assemblyDirectory,"runtimes","win7-x86","native","avutil-56.dll"), new MockFileData("avutil-56.dll") },
                { Path.Combine(assemblyDirectory,"runtimes","win7-x64","native","avutil-56.dll"), new MockFileData("avutil-56.dll") },
                { Path.Combine(assemblyDirectory,"runtimes","linux-x64","native","libavutil.so.56"), new MockFileData("libavutil.so.56") },
                { Path.Combine(assemblyDirectory,"runtimes","osx-x64","native","libavutil.56.dylib"), new MockFileData("libavutil.56.dylib") }
            });

            var macOSBinaries = new MacOSBinaries(fileSystem);

            Assert.Equal(Path.GetFullPath(Path.Combine(assemblyDirectory, "runtimes", "osx-x64", "native", "libavutil.56.dylib")), Path.GetFullPath(macOSBinaries.FindFFmpegLibrary("avutil", 56)));
        }

        [Fact]
        public void FindMacOSBinaries3()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { Path.Combine(assemblyDirectory,"avutil-56.dll"), new MockFileData("avutil-56.dll") },
                { Path.Combine(assemblyDirectory,"libavutil.so.56"), new MockFileData("libavutil.so.56") },
                { Path.Combine(assemblyDirectory,"libavutil.56.dylib"), new MockFileData("libavutil.56.dylib") }
            });

            var macOSBinaries = new MacOSBinaries(fileSystem);

            Assert.Equal(Path.GetFullPath(Path.Combine(assemblyDirectory, "libavutil.56.dylib")), Path.GetFullPath(macOSBinaries.FindFFmpegLibrary("avutil", 56)));
        }
    }
}
