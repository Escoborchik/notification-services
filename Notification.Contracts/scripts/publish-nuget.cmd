@echo off
setlocal enabledelayedexpansion
REM enabledelayedexpansion Ч позвол€ет использовать !VAR! внутри циклов
REM setlocal Ч изол€ци€ переменных внутри скрипта

REM ---------------------------------------
REM 1. ќпредел€ем директорию скрипта
REM ---------------------------------------
set SCRIPT_DIR=%~dp0
REM %0  = им€ скрипта (может быть без пути)
REM %~dp0 = диск (d) + путь (p) к скрипту, всегда абсолютный путь
echo %SCRIPT_DIR%
cd %SCRIPT_DIR%

REM ---------------------------------------
REM 2. ¬ерси€ пакета
REM ---------------------------------------
set VERSION=1.0.3

REM ---------------------------------------
REM 3. „тение API ключа из .env
REM ---------------------------------------
if not exist ..\.env (
    echo ERROR: .env file not found!
    exit /b 1
)

REM „итаем строки вида KEY=VALUE
REM tokens=1,* Ч перва€ часть до '=', всЄ остальное после '='
REM %%a = им€ ключа, %%b = значение
REM delims== Ч разделитель '='
REM usebackq Ч позвол€ет использовать кавычки как им€ файла
REM /i Ч сравнение без учЄта регистра
for /f "usebackq tokens=1,* delims==" %%a in ("..\.env") do (
    
    if /i "%%a"=="NUGET_API_KEY" (
        set NUGET_KEY=%%b
    )

    if /i "%%a"=="NUGET_SOURCE" (
        set NUGET_SOURCE=%%b
    )
)

if "%NUGET_KEY%"=="" (
    echo ERROR: NUGET_API_KEY not found in .env
    exit /b 1
)

echo Source: %NUGET_SOURCE%
echo.

REM ---------------------------------------
REM 4. —писок проектов
REM ---------------------------------------
REM ^ Ч escape-символ, позвол€ющий переносить команду на следующую строку
set PROJECTS=^
Notifications.Contracts/Notifications.Contracts.csproj

REM “о же самое дл€ имЄн пакетов
set PACKAGES=^
Notifications.Contracts

REM ---------------------------------------
REM 5. ѕакуем проекты
REM ---------------------------------------
for %%P in (%PROJECTS%) do (
    echo ----------------------------------------
    echo Packing ../%%P

    dotnet pack ../%%P --configuration Release --output %SCRIPT_DIR% ^
        -p:PackageVersion=%VERSION% -p:Version=%VERSION%

    if errorlevel 1 (
        echo ERROR during packing %%P
        exit /b 1
    )
)

REM ---------------------------------------
REM 6. ѕубликуем пакеты
REM ---------------------------------------
for %%N in (%PACKAGES%) do (
    set PACKAGE_NAME=%%N.%VERSION%.nupkg

    echo ----------------------------------------
    echo Publishing !PACKAGE_NAME!

    dotnet nuget push "!PACKAGE_NAME!" --api-key %NUGET_KEY% --source %NUGET_SOURCE%

    if errorlevel 1 (
        echo ERROR during publishing !PACKAGE_NAME!
        exit /b 1
    )
)

echo.
echo DONE.
pause
REM pause Ч чтобы окно не закрывалось при двойном клике
