@echo off
setlocal enabledelayedexpansion

echo ========================================
echo WARNING: This will delete the following:
echo ========================================
echo [ ] ..\SDK\dist
echo [ ] ..\dotnet-tools.json
echo [ ] ..\Data\.generated
echo [ ] %USERPROFILE%\.nuget\packages\stellar.sdk
echo [ ] %USERPROFILE%\.nuget\packages\stellar.tools
echo ========================================

set /p confirm="Are you sure? (Y/N): "
if /i "!confirm!" neq "Y" (
    echo Operation cancelled.
    pause
    exit /b 1
)

echo Starting cleanup...

call :RemoveDir "..\SDK\dist"
call :RemoveFile "..\.config\dotnet-tools.json"
call :RemoveDir "..\Data\.generated"
call :RemoveDir "%USERPROFILE%\.nuget\packages\stellar.sdk"
call :RemoveDir "%USERPROFILE%\.nuget\packages\stellar.tools"

echo.
echo Cleanup completed successfully!
pause
exit /b 0

:RemoveDir
if exist "%~1" (
    echo Removing directory: %~1
    rmdir /s /q "%~1" 2>nul
    if errorlevel 1 (
        echo ERROR: Failed to remove %~1
        attrib -h -s "%~1" 2>nul
        rmdir /s /q "%~1" 2>nul
    )
) else (
    echo Directory not found: %~1
)
exit /b

:RemoveFile
if exist "%~1" (
    echo Removing file: %~1
    del /q "%~1" 2>nul
) else (
    echo File not found: %~1
)
exit /b