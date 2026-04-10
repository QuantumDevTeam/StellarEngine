@echo off
setlocal enabledelayedexpansion

rem pack Cli
echo Packaging Stellar.Cli...
dotnet pack "../src/Stellar.Cli/Stellar.Cli.csproj" -c Release -o "../dist"
if errorlevel 1 (
    echo ERROR: Failed to pack Stellar.Cli
    exit /b 1
)

rem add SDK NuGet source
echo.
call ../../scripts/add_nuget_source.bat LocalStellarSDK
if errorlevel 1 exit /b 1

echo.
echo Successfully pack and add Stellar Cli to NuGet