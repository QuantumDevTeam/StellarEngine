@echo off
setlocal enabledelayedexpansion

rem Kernel packaging
pushd "%~dp0"
cd /d "Kernel/build"
call "package.bat"
if errorlevel 1 (
    echo ERROR: Failed to install Kernel
    popd
    exit /b 1
)
popd

rem SDK packaging
echo.
pushd "%~dp0"
cd /d "SDK/build"
call "package.bat"
if errorlevel 1 (
    echo ERROR: Failed to install SDK
    popd
    exit /b 1
)
popd

rem installing/updating dotnet tool
echo.
pushd "%~dp0"
cd /d SDK
echo Installing/updating Stellar.Tools tool...
dotnet tool update --add-source dist Stellar.Tools 2>nul || (
    dotnet tool install --add-source dist Stellar.Tools
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

rem SDK installing
echo.
echo Installing SDK in system...
dotnet stellar sdk install 2>&1
if errorlevel 9009 (
    echo ERROR: 'stellar' dotnet tool not found or install command return an err
    exit /b 1
)

echo.
echo Successfully install Stellar SDK
