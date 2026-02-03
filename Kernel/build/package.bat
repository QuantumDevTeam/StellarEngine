@echo off
setlocal enabledelayedexpansion

rem pack Kernel
echo Packaging Stellar.Kernel...
dotnet restore "../src/Stellar.Kernel/Stellar.Kernel.csproj"
dotnet pack "../src/Stellar.Kernel/Stellar.Kernel.csproj" -c Release -o "../dist" --no-restore
if errorlevel 1 (
    echo ERROR: Failed to pack Stellar.Kernel
    exit /b 1
)

rem add Kernel NuGet source
echo.
call ../../scripts/add_nuget_source.bat StellarKernel
if errorlevel 1 exit /b 1

echo.
echo Successfully pack and add Stellar Kernel to NuGet