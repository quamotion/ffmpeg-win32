#!/bin/sh
out="$TRAVIS_BUILD_DIR/bin/"
ffmpeg="$TRAVIS_BUILD_DIR/ffmpeg-3.3.4/"
kvazaar="$TRAVIS_BUILD_DIR/kvazaar-1.1.0/"

mkdir -p $out

cp $ffmpeg/libavcodec/libavcodec.57.dylib $out
cp $ffmpeg/libavformat/libavformat.57.dylib $out
cp $ffmpeg/libavutil/libavutil.55.dylib $out
cp $ffmpeg/libswscale/libswscale.4.dylib $out
cp $kvazaar/src/.libs/libkvazaar.3.dylib $out

# Patch the dylib dependencies for all .dylib files in the out directory
for f in "$out/"*.dylib; do
   chmod +w "$f"

   # Skip the first two lines of the otool output, this is just the name of the dylib itself
   dylibs=`otool -L "$f" | tail -n +3 | grep "/Users/travis/build/" | awk -F' ' '{ print $1 }'`

   for dylib in $dylibs; do
     basename=`basename "$dylib"`

     if [ ! -f "$out/lib/$basename" ]; then
        echo "error: The dylib file '$out/$basename' does not exist in the output folder; referenced from $f"
     fi

     # https://www.mikeash.com/pyblog/friday-qa-2009-11-06-linking-and-install-names.html
     install_name_tool -change "$dylib" "@loader_path/$basename" "$f"
   done;

   otool -L "$f"
done