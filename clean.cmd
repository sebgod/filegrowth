@setlocal enabledelayedexpansion

@for %%A in (%~dp0*.csv) do @for /F "usebackq" %%F in (`set /A %%~nA`) do @if %%F GEQ 1 del %%A
dotnet clean