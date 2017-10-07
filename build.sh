#!/bin/sh
dir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
export prefix=$dir/out

mkdir -p $prefix
mkdir -p $prefix/lib/pkgconfig

export CFLAGS="$CFLAGS -I$prefix/include"
export PKG_CONFIG_PATH=$prefix/lib/pkgconfig
export LDFLAGS="$LDFLAGS -L$prefix/lib"

cd kvazaar-1.1.0

if [ "x$CROSS" = "xyes" ]
then
	echo "Cross-building libkvazaar"
	LIBS="-lpthread" ./configure --prefix=$prefix --disable-static --enable-shared --host=x86_64-w64-mingw32 
else
	echo "Building libkvazaar natively"
	LIBS="-lpthread" ./configure --prefix=$prefix --disable-static --enable-shared
fi

make
make install
cd ..

echo Building FFmpeg
cd ffmpeg-3.3.4
flags_cross=""

if [ "x$CROSS" = "xyes" ]
then
	echo "Cross-building FFmpeg"
	flags_cross="--arch=x86_64 --target-os=mingw32 --cross-prefix=x86_64-w64-mingw32- --pkg-config=pkg-config"
else
	echo "Building FFmpeg natively"
fi

flags_generic="--disable-static --enable-shared"
flags_minimal="--disable-programs --disable-doc --disable-avdevice --disable-swresample --disable-postproc --disable-avfilter --disable-postproc --disable-avfilter --disable-network --disable-everything"
flags_mpeg="--enable-encoder=mpeg1video --enable-decoder=mpeg1_vdpau --enable-decoder=mpeg1video"
flags_h264="--enable-decoder=h264 --enable-parser=h264 --enable-demuxer=h264"
flags_h265="--enable-encoder=libkvazaar --enable-libkvazaar"


if [ "x$CUVID" = "xyes" ]
then
	echo "Building FFmpeg with cuvid/dxva2/cuda support"
	flags_generic="$flags_generic --enable-cuvid --enable-dxva2 --enable-cuda"
	flags_mpeg="$flags_mpeg --enable-decoder=mpeg1_cuvid"
	flags_h264="$flags_h264 --enable-decoder=h264_cuvid --enable-hwaccel=h264_cuvid --enable-hwaccel=h264_dxva2"
else
	echo "Building FFmpeg without cuvid/dxva support"
fi

flags="$flags_cross $flags_generic $flags_minimal $flags_mpeg $flags_h264 $flags_h265 --enable-protocol=file"
LIBS="-lpthread" ./configure $flags
make
cd ..
