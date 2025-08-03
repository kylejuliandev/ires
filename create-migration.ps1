#! /usr/bin/env pwsh
param(
    [Parameter(Mandatory=$true)] [string]$MigrationName
)

# Check if dotnet-ef tool is installed
$efTool = & dotnet tool list | Select-String "dotnet-ef"
if (-not $efTool) {
    Write-Error "dotnet-ef tool is not installed. Please install it with 'dotnet tool install dotnet-ef' and try again."
    exit 1
}

# Save current location and set to script directory
$originalLocation = Get-Location
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $scriptDir

try {
    # Run the migration command
    dotnet tool run dotnet-ef migrations add $MigrationName --startup-project .\src\Ires.MigrationService\ --project .\src\Ires.Data\
} finally {
    Set-Location $originalLocation
}
