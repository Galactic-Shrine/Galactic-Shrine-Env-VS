param(
    [string]$OutputPath,
    [string]$Version
)

# Extraire les trois premiers chiffres de la version
if ($Version -match '^(\d+\.\d+\.\d+)') {
    $versionWithoutBuild = $matches[1]
} else {
    Write-Error "Impossible d'extraire les trois premiers chiffres de la version: $Version"
    exit 1
}

# Extraire le numéro de build
$build = $Version.Split('.')[3]

# Récupérez la date actuelle en timestamp Unix
$date = [System.DateTimeOffset]::Now.ToUnixTimeSeconds()

# Définissez le chemin du changelog
$changelogPath = "CHANGELOG.md"

# Créez le contenu JSON
$jsonContent = @{
    build = $build
    version = $versionWithoutBuild
    date = $date
    changelog = $changelogPath
} | ConvertTo-Json

# Enregistrez le fichier JSON
$jsonFilePath = Join-Path $OutputPath "version.json"
$jsonContent | Set-Content -Path $jsonFilePath -Encoding UTF8