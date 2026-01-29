@echo off
setlocal enabledelayedexpansion

echo ========================================
echo Stellar Kernel Packaging Script
echo ========================================

set "KERNEL_PROJECT=../src/Stellar.Kernel/Stellar.Kernel.csproj"
set "DIST_DIR=../dist"
set "NUGET_SOURCE=StellarKernel"

echo.
echo [1/2] Pack Stellar.Kernel NuGet package...
echo.

dotnet pack "%KERNEL_PROJECT%" -c Release -o "%DIST_DIR%" --nologo
if errorlevel 1 (
    echo Error in NuGet package creating
    pause
    exit /b 1
)

echo.
echo [2/2] Adding nuget source
echo.
for %%I in ("%DIST_DIR%") do set "ABSOLUTE_DIST_PATH=%%~fI"

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

echo.
echo ========================================
echo Packaging completed!
echo ========================================
echo.
