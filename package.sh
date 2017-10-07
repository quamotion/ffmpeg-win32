~/dotnet/dotnet restore --version-suffix r$TRAVIS_BUILD_NUMBER
~/dotnet/dotnet pack -c Release --version-suffix r$TRAVIS_BUILD_NUMBER
