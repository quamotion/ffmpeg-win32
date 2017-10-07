wget -nv -nc https://download.microsoft.com/download/1/B/4/1B4DE605-8378-47A5-B01B-2C79D6C55519/dotnet-sdk-2.0.0-osx-x64.tar.gz -O ~/dotnet-sdk-2.0.0-osx-x64.tar.gz
mkdir -p ~/dotnet
tar xzf ~/dotnet-sdk-2.0.0-osx-x64.tar.gz -C ~/dotnet
~/dotnet/dotnet --version
