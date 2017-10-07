#!/bin/sh
dir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
export prefix=$dir/out

mkdir -p $prefix
mkdir -p $prefix/lib/pkgconfig

export CFLAGS="$CFLAGS -I$prefix/include"
export PKG_CONFIG_PATH=$prefix/lib/pkgconfig
export LDFLAGS="$LDFLAGS -L$prefix/lib"
export PKG_CONFIG_DEBUG_SPEW=1

cd kvazaar-1.1.0
LIBS="-lpthread" ./configure --prefix=$prefix --host=x86_64-w64-mingw32 --disable-static --enable-shared
make
make install
cd ..

echo Building FFmpeg
cd ffmpeg-3.3.4
flags_cross=""

if [ "x$CROSS" = "xyes" ]
then
	echo "Cross-building"
	flags_cross="--arch=x86_64 --target-os=mingw32 --cross-prefix=x86_64-w64-mingw32- --pkg-config=pkg-config"
else
	echo "Building natively"
fi

flags_generic="--disable-static --enable-shared --enable-cuda --enable-cuvid --enable-dxva2"
flags_minimal="--disable-programs --disable-doc --disable-avdevice --disable-swresample --disable-postproc --disable-avfilter --disable-postproc --disable-avfilter --disable-network --disable-everything"
flags_mpeg="--enable-encoder=mpeg1video --enable-decoder=mpeg1_cuvid --enable-decoder=mpeg1_vdpau --enable-decoder=mpeg1video"
flags_h264="--enable-decoder=h264 --enable-decoder=h264_cuvid --enable-hwaccel=h264_cuvid --enable-hwaccel=h264_dxva2 --enable-parser=h264 --enable-demuxer=h264"
flags_h265="--enable-encoder=libkvazaar --enable-libkvazaar"
flags="$flags_cross $flags_generic $flags_minimal $flags_mpeg $flags_h264 $flags_h265 --enable-protocol=file"
LIBS="-lpthread" ./configure $flags
make
cd ..
