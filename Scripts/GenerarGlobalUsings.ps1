# Script para generar global using static de todas las clases en Centrex.Funciones
# Uso: .\GenerarGlobalUsings.ps1 -RutaCarpeta "C:\tu\proyecto\Funciones"

param(
    [Parameter(Mandatory=$false)]
    [string]$RutaCarpeta = ".\Funciones"  # Ruta por defecto
)

# Verificar que la carpeta existe
if (-not (Test-Path $RutaCarpeta)) {
    Write-Host "Error: La carpeta '$RutaCarpeta' no existe." -ForegroundColor Red
    exit
}

Write-Host "Analizando archivos .cs en: $RutaCarpeta" -ForegroundColor Green
Write-Host ""

$resultado = @()

# Obtener todos los archivos .cs en la carpeta
Get-ChildItem -Path $RutaCarpeta -Filter "*.cs" -Recurse | ForEach-Object {
    $archivo = $_.FullName
    $nombreArchivo = $_.Name
    
    Write-Host "Procesando: $nombreArchivo" -ForegroundColor Cyan
    
    # Leer el contenido del archivo
    $contenido = Get-Content $archivo -Raw
    
    # Buscar clases estáticas: static class NombreClase
    $patronClaseEstatica = 'static\s+class\s+(\w+)'
    $matchesEstaticas = [regex]::Matches($contenido, $patronClaseEstatica)
    
    if ($matchesEstaticas.Count -gt 0) {
        foreach ($match in $matchesEstaticas) {
            $nombreClase = $match.Groups[1].Value
            $linea = "global using static Centrex.Funciones.$nombreClase;"
            $resultado += $linea
            Write-Host "  ✓ Encontrada clase estática: $nombreClase" -ForegroundColor Green
        }
    }
    
    # Buscar clases públicas normales (por si acaso): public class NombreClase
    $patronClasePublica = 'public\s+class\s+(\w+)'
    $matchesPublicas = [regex]::Matches($contenido, $patronClasePublica)
    
    if ($matchesPublicas.Count -gt 0) {
        foreach ($match in $matchesPublicas) {
            $nombreClase = $match.Groups[1].Value
            # Solo agregar si no es partial (los partial forms no necesitan using static)
            if ($contenido -notmatch "public\s+partial\s+class\s+$nombreClase") {
                $linea = "global using static Centrex.Funciones.$nombreClase;"
                # Evitar duplicados
                if ($resultado -notcontains $linea) {
                    $resultado += $linea
                    Write-Host "  ✓ Encontrada clase pública: $nombreClase" -ForegroundColor Yellow
                }
            }
        }
    }
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Magenta
Write-Host "RESULTADO - Copiar al GlobalUsings.cs:" -ForegroundColor Magenta
Write-Host "========================================" -ForegroundColor Magenta
Write-Host ""

if ($resultado.Count -eq 0) {
    Write-Host "No se encontraron clases estáticas en la carpeta." -ForegroundColor Red
} else {
    # Ordenar alfabéticamente y eliminar duplicados
    $resultado = $resultado | Sort-Object -Unique
    
    # Mostrar en consola
    $resultado | ForEach-Object {
        Write-Host $_ -ForegroundColor Green
    }
    
    # Guardar en archivo
    $archivoSalida = Join-Path (Get-Location) "GlobalUsings_Generated.txt"
    $resultado | Out-File -FilePath $archivoSalida -Encoding UTF8
    
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Magenta
    Write-Host "✓ Archivo generado: $archivoSalida" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Magenta
}
