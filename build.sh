cd ffmpeg-3.3.4
./configure --arch=x86_64 --target-os=mingw32 --cross-prefix=x86_64-w64-mingw32- --disable-static --enable-shared --enable-version3 --enable-cuda --enable-cuvid --enable-dxva2
make
cd ..
