@echo off
setlocal enabledelayedexpansion

rem pack SDK
echo Building Tasks for Stellar.Sdk...
dotnet build "../src/Build/Stellar.Sdk.Tasks/Stellar.Sdk.Tasks.csproj" -c Release
echo Building Analyzer for Stellar.Sdk...
dotnet build "../src/Stellar.ProjectAnalyzer/Stellar.ProjectAnalyzer.csproj" -c Release
echo Packaging Stellar.Sdk...
dotnet pack "../src/Build/Stellar.Sdk/Stellar.Sdk.csproj" -c Release -o "../dist" -p:IsPacking=true
if errorlevel 1 (
    echo ERROR: Failed to pack Stellar.Sdk
    exit /b 1
)

rem add SDK NuGet source
echo.
call ../../scripts/add_nuget_source.bat LocalStellarSDK
if errorlevel 1 exit /b 1

echo.
echo Successfully pack and add Stellar SDK to NuGet
