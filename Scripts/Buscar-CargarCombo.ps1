# ===============================================
# Script: Buscar-CargarCombo.ps1
# Lista todas las llamadas a cargar_combo
# Incluye la línea anterior si hay una variable SQL
# ===============================================

# Ruta de trabajo actual (podés cambiarla manualmente si querés)
$path = Get-Location
$resultado = Join-Path $path "resultado.txt"

# Limpiar archivo previo si existe
if (Test-Path $resultado) { Remove-Item $resultado }

# Buscar todos los archivos .vb en la carpeta y subcarpetas
$archivos = Get-ChildItem -Path $path -Recurse -Filter "*.vb"

foreach ($archivo in $archivos) {
    $lineas = Get-Content -Path $archivo.FullName
    for ($i = 0; $i -lt $lineas.Count; $i++) {
        $linea = $lineas[$i]

        # Detectar llamada a cargar_combo
        if ($linea -match "\bcargar_combo\s*\(") {
            # Ver si usa una variable SQL en lugar de texto directo
            # Ejemplo: cargar_combo(cmb_cliente, sqlstr, basedb, ...)
            $anteriorIncluida = $false
            if ($linea -match ",\s*(sql|query|consulta|cmd|cadena)\w*\s*,") {
                if ($i -gt 0) {
                    $lineaAnterior = $lineas[$i - 1]
                    Add-Content -Path $resultado -Value "[$($archivo.Name)] (línea $($i)):"
                    Add-Content -Path $resultado -Value $lineaAnterior
                    $anteriorIncluida = $true
                }
            }

            # Agregar la línea con cargar_combo
            if (-not $anteriorIncluida) {
                Add-Content -Path $resultado -Value "[$($archivo.Name)] (línea $($i)):"
            }
            Add-Content -Path $resultado -Value $linea
            Add-Content -Path $resultado -Value ""  # Separador en blanco
        }
    }
}

Write-Host "✅ Búsqueda completada. Resultados guardados en: $resultado" -ForegroundColor Green
