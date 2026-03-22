@echo off
setlocal enabledelayedexpansion

rem corCore modules
call :PackEngineModule "Core"
call :PackEngineModule "FileSystem"
call :PackEngineModule "Logging"
call :PackEngineModule "Threading"
call :PackEngineModule "Time"
rem other engine modules
call :PackEngineModule "Assets"
call :PackEngineModule "Network"
call :PackEngineModule "Sounds"
call :PackEngineModule "Graphic"
call :PackEngineModule "Graphic.UI"
call :PackEngineModule "Physics"

rem add StellarEngine NuGet source
echo.
call ../../scripts/add_nuget_source.bat LocalStellarEngineModules
if errorlevel 1 exit /b 1

echo.
echo Successfully pack and add all StellarEngine modules to NuGet
exit /b 0

:PackEngineModule
if exist "%~1" (
    echo Packaging Stellar.%~1...
    dotnet pack "../src/Stellar.%~1/Stellar.%~1.csproj" -c Release -o "../dist" -p:StellarPrecompiled=true
    if errorlevel 1 (
        echo ERROR: Failed to pack Stellar.Core
        exit /b 1
    )
) else (
    echo Module name has no exist: %~1
)
exit /b