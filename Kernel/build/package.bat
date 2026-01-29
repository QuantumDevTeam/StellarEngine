dotnet pack "./src/Stellar.Kernel/Stellar.Kernel.csproj" -c Release -o "../dist"
if errorlevel 1 exit /b 1

../../Data/add_nuget_source.bat StellarKernel