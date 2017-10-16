#!/bin/sh
out="$TRAVIS_BUILD_DIR/out/"
ffmpeg="$TRAVIS_BUILD_DIR/ffmpeg-3.3.4/"
kvazaar="$TRAVIS_BUILD_DIR/kvazaar-1.1.0/"

mkdir -p $out

cp $ffmpeg/libavcodec/libavcodec.so.57 $out
cp $ffmpeg/libavformat/libavformat.so.57 $out
cp $ffmpeg/libavutil/libavutil.so.55 $out
cp $ffmpeg/libswscale/libswscale.so.4 $out
cp $kvazaar/src/.libs/libkvazaar.so.3 $out

# For more info, see:
# https://github.com/NixOS/patchelf
# http://man7.org/linux/man-pages/man8/ld.so.8.html

patchelf=patchelf-0.9/src/patchelf

for f in $out/*; do
   chmod +w $f

   $patchelf --set-rpath '${ORIGIN}' $f

   readelf -d $f
done