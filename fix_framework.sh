#!/bin/bash
# fix_all_frameworks.sh - Fuerza netcoreapp2.1 en todos los proyectos

project_dir="csharp-text_based_interface"

if [ ! -d "$project_dir" ]; then
    echo "❌ Error: No existe el directorio $project_dir"
    exit 1
fi

echo "🔍 Corrigiendo .csproj a netcoreapp2.1 en $project_dir..."
find "$project_dir" -name "*.csproj" | while read -r file; do
    echo "🛠️ Editando: $file"
    # Forzar el SDK a Microsoft.NET.Sdk
    sed -i 's#<Project Sdk=".*">#<Project Sdk="Microsoft.NET.Sdk">#' "$file"
    # Forzar TargetFramework único
    sed -i 's#<TargetFrameworks>.*</TargetFrameworks>#<TargetFramework>netcoreapp2.1</TargetFramework>#' "$file"
    sed -i 's#<TargetFramework>.*</TargetFramework>#<TargetFramework>netcoreapp2.1</TargetFramework>#' "$file"
done

echo "🔧 Reconstruyendo solución..."
dotnet build "$project_dir/InventoryManagement.sln"
echo "🧪 Ejecutando tests..."
dotnet test "$project_dir/InventoryManagement.Tests/InventoryManagement.Tests.csproj"

