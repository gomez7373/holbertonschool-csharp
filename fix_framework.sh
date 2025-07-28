#!/bin/bash
# fix_framework.sh - Ajusta TargetFramework y versiones de xUnit para compatibilidad con el checker de Holberton

project_dir="csharp-text_based_interface"
tests_proj="InventoryManagement.Tests/InventoryManagement.Tests.csproj"

if [ ! -d "$project_dir" ]; then
    echo "❌ Error: No existe el directorio $project_dir. Ejecútalo desde holbertonschool-csharp/"
    exit 1
fi

echo "🔍 Buscando archivos .csproj en $project_dir..."
find "$project_dir" -name "*.csproj" | while read -r file; do
    echo "🛠️ Corrigiendo TargetFramework en: $file"
    # Forzar el SDK a Microsoft.NET.Sdk
    sed -i 's#<Project Sdk=".*">#<Project Sdk="Microsoft.NET.Sdk">#' "$file"
    # Forzar TargetFramework único
    sed -i 's#<TargetFrameworks>.*</TargetFrameworks>#<TargetFramework>netcoreapp2.1</TargetFramework>#' "$file"
    sed -i 's#<TargetFramework>.*</TargetFramework>#<TargetFramework>netcoreapp2.1</TargetFramework>#' "$file"
done

# Arreglar versiones de xUnit en el proyecto de tests
if [ -f "$project_dir/$tests_proj" ]; then
    echo "🛠️ Ajustando xUnit a versiones compatibles en: $tests_proj"
    sed -i 's#<PackageReference Include="xunit" Version=".*" />#<PackageReference Include="xunit" Version="2.4.1" />#' "$project_dir/$tests_proj"
    sed -i 's#<PackageReference Include="xunit.runner.visualstudio" Version=".*" />#<PackageReference Include="xunit.runner.visualstudio" Version="2.4.3" />#' "$project_dir/$tests_proj"
fi

echo "🔧 Restaurando paquetes..."
dotnet restore "$project_dir/InventoryManagement.sln"

echo "🔨 Reconstruyendo solución..."
dotnet build "$project_dir/InventoryManagement.sln"

echo "🧪 Ejecutando tests..."
dotnet test "$project_dir/$tests_proj"

echo "✅ Framework y versiones corregidas. Proyecto listo."

