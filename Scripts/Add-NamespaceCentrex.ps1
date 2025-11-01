# ===============================================
# Agregar namespace Centrex.Models a todas las entidades VB
# ===============================================

# Carpeta donde están las entidades
$path = "S:\Users\Javi\Desktop\Centrex 2.0 - copia\Models"  # <-- Cambiá esta ruta si hace falta

# Obtener todos los archivos .vb
$files = Get-ChildItem -Path $path -Filter "*.vb" -Recurse

foreach ($file in $files) {
    Write-Host "Procesando: $($file.FullName)" -ForegroundColor Cyan

    $lines = Get-Content -Path $file.FullName -Raw -Encoding UTF8
    $originalLines = $lines -split "`r?`n"

    # Saltar si ya tiene namespace
    if ($lines -match "Namespace\s+Centrex\.Models") {
        Write-Host "→ Ya contiene Namespace, se omite." -ForegroundColor Yellow
        continue
    }

    # Detectar si la primera línea es un comentario
    $firstLine = $originalLines[0]
    $hasComment = $firstLine.TrimStart().StartsWith("'")

    $newLines = @()

    if ($hasComment) {
        # Mantener la primera línea fuera del namespace
        $newLines += $firstLine
        $newLines += ""  # Línea vacía
        $newLines += "Namespace Centrex.Models"
        # Indentar todo lo demás (excepto la primera)
        $rest = $originalLines[1..($originalLines.Count - 1)] | ForEach-Object { "`t$_" }
        $newLines += $rest
        $newLines += "End Namespace"
    } else {
        # No hay comentario, todo dentro del namespace
        $newLines += "Namespace Centrex.Models"
        $rest = $originalLines | ForEach-Object { "`t$_" }
        $newLines += $rest
        $newLines += "End Namespace"
    }

    # Escribir de nuevo el archivo
    $newLines | Set-Content -Path $file.FullName -Encoding UTF8

    Write-Host "✔ Namespace agregado correctamente." -ForegroundColor Green
}

Write-Host "`n✅ Proceso completado para $($files.Count) archivos." -ForegroundColor Green
