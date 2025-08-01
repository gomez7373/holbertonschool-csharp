#!/bin/bash
# fix_framework.sh - Ajusta TargetFramework, xUnit y limpia archivos innecesarios

project_dir="csharp-text_based_interface"
tests_proj="$project_dir/InventoryManagement.Tests/InventoryManagement.Tests.csproj"

if [ ! -d "$project_dir" ]; then
    echo "❌ Error: No existe el directorio $project_dir. Ejecútalo desde holbertonschool-csharp/"
    exit 1
fi

echo "🧹 Eliminando Class1.cs innecesario..."
rm -f "$project_dir/InventoryLibrary/Class1.cs"

echo "🔍 Corrigiendo todos los .csproj a netcoreapp2.1..."
find "$project_dir" -name "*.csproj" | while read -r file; do
    echo "🛠️ Modificando: $file"
    sed -i 's#<Project Sdk=".*">#<Project Sdk="Microsoft.NET.Sdk">#' "$file"
    sed -i 's#<TargetFrameworks>.*</TargetFrameworks>#<TargetFramework>netcoreapp2.1</TargetFramework>#' "$file"
    sed -i 's#<TargetFramework>.*</TargetFramework>#<TargetFramework>netcoreapp2.1</TargetFramework>#' "$file"
    sed -i '/<Nullable>.*<\/Nullable>/d' "$file"
done

echo "🛠️ Ajustando xUnit a versiones compatibles..."
if [ -f "$tests_proj" ]; then
    sed -i '/xunit/d' "$tests_proj"
    sed -i '/<\/ItemGroup>/i \ \ \ \ <PackageReference Include="xunit" Version="2.4.1" />\n \ \ \ \ <PackageReference Include="xunit.runner.visualstudio" Version="2.4.3" />' "$tests_proj"
fi

echo "🔧 Restaurando paquetes..."
dotnet restore "$project_dir/InventoryManagement.sln"

echo "🔨 Compilando solución..."
dotnet build "$project_dir/InventoryManagement.sln"

echo "🧪 Ejecutando tests..."
dotnet test "$tests_proj"

echo "✅ Proyecto corregido y tests ejecutados."

