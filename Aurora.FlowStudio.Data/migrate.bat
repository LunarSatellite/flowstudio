@echo off
REM Aurora FlowStudio - Database Migration Script (Windows)
REM This script creates and applies EF Core migrations

echo ======================================
echo Aurora FlowStudio - Database Migration
echo ======================================
echo.

REM Check if migration name is provided
if "%1"=="" (
    set MIGRATION_NAME=InitialCreate
    echo No migration name provided. Using default: %MIGRATION_NAME%
) else (
    set MIGRATION_NAME=%1
    echo Migration name: %MIGRATION_NAME%
)

echo.
echo Step 1: Adding migration...
dotnet ef migrations add %MIGRATION_NAME% --project Aurora.FlowStudio.Data.csproj --verbose

if errorlevel 1 (
    echo.
    echo X Error: Failed to add migration
    exit /b 1
)

echo.
echo V Migration added successfully!
echo.
echo Step 2: Applying migration to database...
dotnet ef database update --project Aurora.FlowStudio.Data.csproj --verbose

if errorlevel 1 (
    echo.
    echo X Error: Failed to apply migration
    exit /b 1
)

echo.
echo V Database updated successfully!
echo.
echo ======================================
echo Migration completed!
echo ======================================
pause
