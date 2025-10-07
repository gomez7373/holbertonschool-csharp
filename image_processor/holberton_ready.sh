#!/bin/bash
# 🧹 Holberton Ready Script — by SGC & ChatGPT mentor mode
# Prepara el proyecto exactamente como lo espera el checker oficial

echo "🔍 Paso 1: Limpiando archivos temporales..."
rm -rf bin obj images *.zip *.png *.jpg 2>/dev/null

echo "🧠 Paso 2: Verificando estructura requerida..."
required_files=("ImageProcessor.cs" "ImageProcessor.csproj" "main.cs" "README.md")
for f in "${required_files[@]}"; do
  if [ ! -f "$f" ]; then
    echo "❌ Falta el archivo obligatorio: $f"
    exit 1
  fi
done
echo "✅ Todos los archivos requeridos están presentes."

echo "🧾 Paso 3: Normalizando formato de texto..."
# Elimina BOM y asegura salto de línea final
for file in ImageProcessor.cs README.md main.cs; do
  iconv -f utf-8 -t utf-8 -c "$file" -o "$file.tmp" && mv "$file.tmp" "$file"
  dos2unix "$file" 2>/dev/null || true
  printf "\n" >> "$file"
done
echo "✅ Archivos codificados en UTF-8 sin BOM y con saltos de línea."

echo "🧩 Paso 4: Confirmando firmas de métodos..."
grep -q "public class ImageProcessor" ImageProcessor.cs || { echo "❌ Falta clase ImageProcessor"; exit 1; }
grep -q "public static void Inverse(string" ImageProcessor.cs || { echo "❌ Falta método Inverse"; exit 1; }
grep -q "public static void Grayscale(string" ImageProcessor.cs || { echo "❌ Falta método Grayscale"; exit 1; }
grep -q "public static void BlackWhite(string" ImageProcessor.cs || { echo "❌ Falta método BlackWhite"; exit 1; }
grep -q "public static void Thumbnail(string" ImageProcessor.cs || { echo "❌ Falta método Thumbnail"; exit 1; }
echo "✅ Métodos detectados correctamente."

echo "⚙️ Paso 5: Restaurando dependencias..."
dotnet clean >/dev/null
dotnet restore >/dev/null

echo "🏗️ Paso 6: Compilando con framework Holberton (netcoreapp2.1)..."
dotnet build >/dev/null
if [ $? -ne 0 ]; then
  echo "❌ Error al compilar. Revisa errores antes de hacer push."
  exit 1
fi
echo "✅ Compilación completada exitosamente."

echo "📁 Estructura final lista para push:"
ls -1A | grep -v "holberton_ready.sh"

echo "🎯 Proyecto 100% listo para el checker Holberton."
echo "Consejo final: git add ., git commit, git push 🚀"

