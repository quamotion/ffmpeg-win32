cd ffmpeg-3.3.4
./configure --arch=x86_64 --target-os=mingw32 --cross-prefix=x86_64-w64-mingw32- --disable-static --enable-shared --enable-cuda --enable-cuvid --enable-dxva2 --disable-programs --disable-doc --disable-avdevice --disable-swresample --disable-postproc --disable-avfilter --disable-postproc --disable-avfilter --disable-network --disable-everything --enable-decoder=h264 --enable-decoder=h264_cuvid --enable-hwaccel=h264_cuvid --enable-hwaccel=h264_dxva2 --enable-parser=h264 --enable-demuxer=h264
make
cd ..
