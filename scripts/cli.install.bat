@echo off
setlocal enabledelayedexpansion

rem installing/updating dotnet tool
pushd "%~dp0"
cd /d ../SDK
echo Installing/updating Stellar.Cli tool...
dotnet tool update Stellar.Cli --add-source dist 2>nul || (
    dotnet tool install Stellar.Cli --add-source dist --create-manifest-if-needed
)
if errorlevel 1 (
    echo ERROR: Failed to install/update Stellar.Cli tool
    echo Check if Stellar.Cli.nupkg exists in dist folder
    dir dist\*.nupkg
    popd
    exit /b 1
)

rem verifying dotnet tool
echo.
echo Verifying Cli installation...
dotnet tool list | findstr /i "stellar"
if errorlevel 1 (
    echo ERROR: Stellar.Cli not found in tools list
    popd
    exit /b 1
)
popd