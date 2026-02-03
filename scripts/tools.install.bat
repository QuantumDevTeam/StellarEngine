@echo off
setlocal enabledelayedexpansion

rem installing/updating dotnet tool
pushd "%~dp0"
cd /d ../SDK
echo Installing/updating Stellar.Tools tool...
dotnet tool update Stellar.Tools --add-source dist 2>nul || (
    dotnet tool install Stellar.Tools --add-source dist --create-manifest-if-needed
)
if errorlevel 1 (
    echo ERROR: Failed to install/update Stellar.Tools tool
    echo Check if Stellar.Tools.nupkg exists in dist folder
    dir dist\*.nupkg
    popd
    exit /b 1
)

rem verifying dotnet tool
echo.
echo Verifying Tools installation...
dotnet tool list | findstr /i "stellar"
if errorlevel 1 (
    echo ERROR: Stellar.Tools not found in tools list
    popd
    exit /b 1
)
popd