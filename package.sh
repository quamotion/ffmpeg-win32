~/dotnet/dotnet restore /p:VersionSuffix=r$TRAVIS_BUILD_NUMBER
~/dotnet/dotnet pack -c Release --version-suffix r$TRAVIS_BUILD_NUMBER
