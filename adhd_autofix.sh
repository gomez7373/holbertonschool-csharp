#!/bin/bash
# adhd_autofix.sh - Script para reparar y preparar el proyecto C# Text-based Interface para el checker

project_dir="csharp-text_based_interface"
solution="$project_dir/InventoryManagement.sln"
tests_proj="$project_dir/InventoryManagement.Tests/InventoryManagement.Tests.csproj"
library_proj="$project_dir/InventoryLibrary/InventoryLibrary.csproj"
jsonstorage_file="$project_dir/InventoryLibrary/JSONStorage.cs"

if [ ! -d "$project_dir" ]; then
    echo "❌ Error: No existe el directorio $project_dir. Ejecútalo desde holbertonschool-csharp/"
    exit 1
fi

echo "🧹 Limpiando archivos innecesarios..."
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

echo "📦 Instalando Newtonsoft.Json..."
dotnet add "$library_proj" package Newtonsoft.Json --version 12.0.3

echo "📝 Reescribiendo JSONStorage.cs con Newtonsoft.Json..."
cat > "$jsonstorage_file" <<'EOF'
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace InventoryLibrary
{
    /// <summary>
    /// Storage manager using JSON (Newtonsoft.Json)
    /// </summary>
    public class JSONStorage
    {
        public Dictionary<string, object> objects { get; set; } = new Dictionary<string, object>();
        private readonly string filePath = Path.Combine("storage", "inventory_manager.json");

        public Dictionary<string, object> All()
        {
            return objects;
        }

        public void New(object obj)
        {
            string key = obj.GetType().Name + "." + obj.GetType().GetProperty("id").GetValue(obj).ToString();
            objects[key] = obj;
        }

        public void Save()
        {
            Directory.CreateDirectory("storage");
            string json = JsonConvert.SerializeObject(objects, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public void Load()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                objects = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            }
        }
    }
}
EOF

echo "🧹 Eliminando tipos nullable en modelos..."
find "$project_dir/InventoryLibrary" -type f -name "*.cs" -exec sed -i 's/?//g' {} \;

echo "🔧 Restaurando paquetes..."
dotnet restore "$solution"

echo "🔨 Compilando solución..."
dotnet build "$solution"

echo "🧪 Ejecutando tests..."
dotnet test "$tests_proj"

echo "✅ Proyecto preparado correctamente para el checker."

