@echo off
setlocal enabledelayedexpansion

rem pack Tools
echo Packaging Stellar.Tools...
dotnet pack "../src/Stellar.Tools/Stellar.Tools.csproj" -c Release -o "../dist"
if errorlevel 1 (
    echo ERROR: Failed to pack Stellar.Tools
    exit /b 1
)

rem add SDK NuGet source
echo.
call ../../scripts/add_nuget_source.bat StellarSDK
if errorlevel 1 exit /b 1

echo.
echo Successfully pack and add Stellar Tools to NuGet