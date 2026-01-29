@echo off
setlocal enabledelayedexpansion

echo ========================================
echo Stellar SDK Packaging Script
echo ========================================

set "TASKS_PROJECT=../src/Build/Stellar.Sdk.Tasks/Stellar.Sdk.Tasks.csproj"
set "SDK_PROJECT=../src/Build/Stellar.Sdk/Stellar.Sdk.csproj"
set "DIST_DIR=../dist"
set "TEMPLATE_DIR=../../Data/SdkTemplate"
set "NUGET_SOURCE=StellarSDK"

echo.
echo [1/4] Building Stellar.Sdk...
echo.

dotnet build "%SDK_PROJECT%" -c Release --nologo
if errorlevel 1 (
    echo Stellar.Sdk build error
    pause
    exit /b 1
)

echo.
echo [2/4] Creating NuGet package...
echo.

dotnet pack "%SDK_PROJECT%" -c Release -o "%DIST_DIR%" --nologo
if errorlevel 1 (
    echo Error in NuGet package creating
    pause
    exit /b 1
)

echo.
echo [3/4] Copying system templates...

for /f "tokens=*" %%v in ('dotnet --version') do set SDK_VERSION=%%v
if "!SDK_VERSION!"=="" (
    echo Cannot get .NET SDK version
    pause
    exit /b 1
)

echo.
echo Find .NET SDK version: %SDK_VERSION%

set "SDK_PATH=%USERPROFILE%\.dotnet\sdk-advertising\!SDK_VERSION!\stellar.sdk"
echo SDK path will be: !SDK_PATH!

if not exist "!SDK_PATH!\" (
    mkdir "!SDK_PATH!"
    echo Created SDK directory: !SDK_PATH!
)

echo.

if not exist "%TEMPLATE_DIR%\" (
    echo WARNING: Template directory not found: %TEMPLATE_DIR%
) else (
    if exist "%TEMPLATE_DIR%\AdvertisedManifestFeatureBand.txt" (
        copy "%TEMPLATE_DIR%\AdvertisedManifestFeatureBand.txt" "!SDK_PATH!\" >nul
        echo Copied: AdvertisedManifestFeatureBand.txt
    ) else (
        echo WARNING: File not found: AdvertisedManifestFeatureBand.txt
    )
    
    if exist "%TEMPLATE_DIR%\WorkloadManifest.json" (
        copy "%TEMPLATE_DIR%\WorkloadManifest.json" "!SDK_PATH!\" >nul
        echo Copied: WorkloadManifest.json
    ) else (
        echo WARNING: File not found: WorkloadManifest.json
    )
    
    if exist "%TEMPLATE_DIR%\WorkloadManifest.targets" (
        copy "%TEMPLATE_DIR%\WorkloadManifest.targets" "!SDK_PATH!\" >nul
        echo Copied: WorkloadManifest.targets
    ) else (
        echo WARNING: File not found: WorkloadManifest.targets
    )
)

echo.
echo [4/4] Adding nuget source
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
