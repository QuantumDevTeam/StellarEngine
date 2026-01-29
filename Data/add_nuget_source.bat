@echo off
setlocal enabledelayedexpansion

if "%~1"=="" (
    echo ERROR: NuGet source name is not specified
    exit /b 1
)

set "NUGET_SOURCE=%1"

for %%I in ("../dist") do set "ABSOLUTE_DIST_PATH=%%~fI"

dotnet nuget list source | findstr /C:"%NUGET_SOURCE%" >nul
if errorlevel 1 (
    echo NuGet source not found, adding new source...
    dotnet nuget add source "!ABSOLUTE_DIST_PATH!" -n "%NUGET_SOURCE%"
    if errorlevel 1 (
        echo WARNING: Failed to add NuGet source
    ) else (
        echo Successfully added NuGet source: "%NUGET_SOURCE%"
    )
) else (
    echo NuGet source "%NUGET_SOURCE%" already exists, updating nuget source...
   
	dotnet nuget remove source "%NUGET_SOURCE%" >nul 2>&1
	dotnet nuget add source "!ABSOLUTE_DIST_PATH!" -n "%NUGET_SOURCE%"
	
	if errorlevel 1 (
		echo WARNING: Failed to update NuGet source
	) else (
		echo Successfully updated NuGet source: "%NUGET_SOURCE%"
	)
)
echo.
echo Verifying NuGet source...
echo.

dotnet nuget list source | findstr /C:"%NUGET_SOURCE%" >nul
if errorlevel 1 (
    echo ERROR: NuGet source verification failed
) else (
    echo NuGet source verified successfully
)
