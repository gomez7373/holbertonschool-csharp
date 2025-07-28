#!/bin/bash
# Fix TargetFramework for Holberton checkers (force netcoreapp2.1)

project_dir="csharp-text_based_interface"

if [ ! -d "$project_dir" ]; then
    echo "❌ Error: No existe el directorio $project_dir. Ejecútalo desde holbertonschool-csharp/"
    exit 1
fi

echo "🔍 Buscando archivos .csproj en $project_dir..."
find "$project_dir" -name "*.csproj" | while read -r file; do
    if grep -q "<TargetFramework>" "$file"; then
        echo "🛠️ Corrigiendo: $file"
        sed -i 's#<TargetFramework>.*</TargetFramework>#<TargetFramework>netcoreapp2.1</TargetFramework>#' "$file"
    fi
done

echo "✅ Todos los proyectos ahora usan netcoreapp2.1."

