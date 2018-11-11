@setlocal enabledelayedexpansion

:: Launch profile is used by Visual Studio to aovid the \bin\Debug path issue
dotnet run --no-launch-profile -p "%~dp0FileGrowthConsoleApp"