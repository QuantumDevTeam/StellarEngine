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

rem SDK installing
echo.
echo Installing SDK in system...
dotnet stellar install-sdk 2>&1
if errorlevel 9009 (
    echo ERROR: 'stellar' dotnet tool not found or install command return an err
    exit /b 1
)

echo.
echo Successfully install Stellar SDK
