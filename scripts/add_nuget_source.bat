@echo off
setlocal enabledelayedexpansion

if "%~1"=="" (
    echo ERROR: NuGet source name is not specified
    exit /b 1
)

set "NUGET_SOURCE=%1"
for %%I in ("../dist") do set "ABSOLUTE_DIST_PATH=%%~fI"

echo Configuring NuGet source: "%NUGET_SOURCE%"

dotnet nuget list source | findstr /C:"%NUGET_SOURCE%" >nul 2>&1
if errorlevel 1 (
    dotnet nuget add source "!ABSOLUTE_DIST_PATH!" -n "%NUGET_SOURCE%" >nul 2>&1
) else (
    dotnet nuget remove source "%NUGET_SOURCE%" >nul 2>&1
    dotnet nuget add source "!ABSOLUTE_DIST_PATH!" -n "%NUGET_SOURCE%" >nul 2>&1
)
if errorlevel 1 (
    echo ERROR: Failed to configure NuGet source: "%NUGET_SOURCE%"
    exit /b 1
)

echo Successfully configured NuGet source: "%NUGET_SOURCE%"