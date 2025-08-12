Requirements to get started on ARM macs

brew install freetype libpng
brew install freeimage

# Path to the ARM64 file Homebrew installed
FREETYPE_ARM64_LIB="/opt/homebrew/opt/freetype/lib/libfreetype.6.dylib"
FREEIMAGE_ARM64_LIB="/opt/homebrew/opt/freetype/lib/libfreeimage.dylib"

# Destination directory mgcb probes
MGCB_NATIVE="$HOME/.nuget/packages/dotnet-mgcb/3.8.2.1105/tools/net8.0/any/runtimes/osx/native"

# Make sure the folder exists
mkdir -p "$MGCB_NATIVE"/

# Copy and rename to the expected filename
cp "$FREETYPE_ARM64_LIB" "$MGCB_NATIVE/libfreetype6.dylib"
cp "$FREEIMAGE_ARM64_LIB" "$MGCB_NATIVE/libfreeimage.dylib"