@echo off
setlocal enabledelayedexpansion

rem Tools packaging
pushd "%~dp0"
cd /d "SDK/build"
call "Tools.package.bat"
if errorlevel 1 (
    echo ERROR: Failed to pack Tools
    popd
    exit /b 1
)
popd

rem installing/updating dotnet tool
echo.
pushd "%~dp0"
cd /d "scripts"
call "tools.install.bat"
if errorlevel 1 (
    echo ERROR: Failed to pack Kernel
    popd
    exit /b 1
)
popd

rem Kernel packaging
echo.
pushd "%~dp0"
cd /d "Kernel/build"
call "package.bat"
if errorlevel 1 (
    echo ERROR: Failed to pack Kernel
    popd
    exit /b 1
)
popd

rem SDK packaging
echo.
pushd "%~dp0"
cd /d "SDK/build"
call "Sdk.package.bat"
if errorlevel 1 (
    echo ERROR: Failed to pack SDK
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
