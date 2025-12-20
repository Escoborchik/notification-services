$ErrorActionPreference = "Stop"

function LogInfo([string]$msg)  { Write-Host ("[{0}] [INFO ] {1}" -f (Get-Date -Format "yyyy-MM-dd HH:mm:ss"), $msg) }
function LogOk([string]$msg)    { Write-Host ("[{0}] [OK   ] {1}" -f (Get-Date -Format "yyyy-MM-dd HH:mm:ss"), $msg) }
function LogWarn([string]$msg)  { Write-Host ("[{0}] [WARN ] {1}" -f (Get-Date -Format "yyyy-MM-dd HH:mm:ss"), $msg) }
function LogErr([string]$msg)   { Write-Host ("[{0}] [ERROR] {1}" -f (Get-Date -Format "yyyy-MM-dd HH:mm:ss"), $msg) }

$startup = "..\EmailNotificationService"

$contexts = @(
    @{
        Name = "EmailNotificationService"
        Project = "..\EmailNotificationService"
        Context = "AppDbContext"
        Migration = "InitEmailNotificationService"
    }
)

function RunStep([string]$title, [scriptblock]$action) {
    LogInfo $title
    try {
        $sw = [System.Diagnostics.Stopwatch]::StartNew()
        & $action
        $sw.Stop()
        LogOk ("{0} (took {1} ms)" -f $title, $sw.ElapsedMilliseconds)
    }
    catch {
        LogErr ("{0} failed: {1}" -f $title, $_.Exception.Message)
        throw
    }
}

LogInfo "Start migrations reset"
LogInfo ("Startup project: {0}" -f $startup)
LogInfo ("Contexts: {0}" -f ($contexts.Name -join ", "))

# 1) Remove migrations
foreach ($c in $contexts) {
    $migrationsPath = Join-Path $c.Project "Migrations"
    if (Test-Path $migrationsPath) {
        RunStep ("Remove migrations for {0}: {1}" -f $c.Name, $migrationsPath) {
            Remove-Item $migrationsPath -Recurse -Force
        }
    } else {
        LogWarn ("Migrations folder not found for {0}: {1}" -f $c.Name, $migrationsPath)
    }
}


# 3) Add migrations + update database for each context
foreach ($c in $contexts) {

    LogInfo ("=== {0} ===" -f $c.Name)

    RunStep ("Add migration {0} ({1})" -f $c.Migration, $c.Context) {
        dotnet ef migrations add `
            $c.Migration `
            -s $startup `
            -p $c.Project `
            -c $c.Context
    }

    RunStep ("Update database ({0})" -f $c.Context) {
        dotnet ef database update `
            -s $startup `
            -p $c.Project `
            -c $c.Context
    }
}

LogOk "Done"

