dotnet pack "../src/Build/Stellar.Sdk/Stellar.Sdk.csproj" -c Release -o "../dist" -p:IsPacking=true
if errorlevel 1 exit /b 1

../../Data/add_nuget_source.bat StellarSDK

dotnet tool update --add-source "../dist" Stellar.Tools || ^
dotnet tool install --add-source "../dist" Stellar.Tools

stellar install-sdk