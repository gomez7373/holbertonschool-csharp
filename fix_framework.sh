#!/bin/bash
# fix_framework.sh - Ajusta TargetFramework y fuerza versiones correctas de xUnit para Holberton

project_dir="csharp-text_based_interface"
tests_proj="$project_dir/InventoryManagement.Tests/InventoryManagement.Tests.csproj"

if [ ! -d "$project_dir" ]; then
    echo "❌ Error: No existe el directorio $project_dir. Ejecútalo desde holbertonschool-csharp/"
    exit 1
fi

echo "🔍 Corrigiendo todos los .csproj a netcoreapp2.1..."
find "$project_dir" -name "*.csproj" | while read -r file; do
    echo "🛠️ Modificando: $file"
    # Forzar el SDK a Microsoft.NET.Sdk
    sed -i 's#<Project Sdk=".*">#<Project Sdk="Microsoft.NET.Sdk">#' "$file"
    # Forzar TargetFramework único
    sed -i 's#<TargetFrameworks>.*</TargetFrameworks>#<TargetFramework>netcoreapp2.1</TargetFramework>#' "$file"
    sed -i 's#<TargetFramework>.*</TargetFramework>#<TargetFramework>netcoreapp2.1</TargetFramework>#' "$file"
done

echo "🛠️ Ajustando xUnit a versiones compatibles..."
if [ -f "$tests_proj" ]; then
    # Eliminar cualquier línea existente de xunit
    sed -i '/xunit/d' "$tests_proj"
    # Insertar referencias correctas antes del cierre de ItemGroup
    sed -i '/<\/ItemGroup>/i \ \ \ \ <PackageReference Include="xunit" Version="2.4.1" />\n \ \ \ \ <PackageReference Include="xunit.runner.visualstudio" Version="2.4.3" />' "$tests_proj"
fi

echo "🔧 Restaurando paquetes..."
dotnet restore "$project_dir/InventoryManagement.sln"

echo "🔨 Compilando solución..."
dotnet build "$project_dir/InventoryManagement.sln"

echo "🧪 Ejecutando tests..."
dotnet test "$tests_proj"

echo "✅ Proyecto corregido y tests ejecutados."

