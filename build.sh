cd ffmpeg-3.3.4
./configure --arch=x86_64 --target-os=mingw32 --cross-prefix=x86_64-w64-mingw32- --disable-static --enable-shared --disable-programs --disable-doc --disable-avdevice --disable-network
make
