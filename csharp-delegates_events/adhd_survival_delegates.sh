#!/bin/bash

# ADHD-friendly Holberton delegate+event project setup

# 🎉 Function to display a success message
function congratulate() {
  echo "✅ Well done! The task '$1' was created and compiled successfully!"
}

# 🗂️ Step 1: Prompt for folder and source file name
read -p "📁 Enter task folder name (e.g., 0-universal_health): " task_name
read -p "📄 Enter main source file name (e.g., 0-universal_health.cs): " source_file

# 🛠️ Step 2: Create .NET console project
echo "📦 Creating .NET console app in '$task_name'..."
dotnet new console -n "$task_name"
cd "$task_name" || exit 1

# 📝 Step 3: Rename Program.cs to source file
if [ -f "Program.cs" ]; then
  mv Program.cs "$source_file"
  echo "✅ Renamed Program.cs to $source_file"
else
  echo "❌ Error: Program.cs not found!"
  exit 1
fi

# 🔧 Step 4: Configure .csproj for Holberton
csproj_file="${task_name}.csproj"
xml_output_name="${csproj_file%.csproj}.xml"
if [ -f "$csproj_file" ]; then
  echo "🛠️ Updating $csproj_file..."
  cat > "$csproj_file" <<EOL
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp2.1</TargetFramework>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <DocumentationFile>bin/Debug/netcoreapp2.1/${xml_output_name}</DocumentationFile>
  </PropertyGroup>
</Project>
EOL
  echo "✅ Project file configured"
else
  echo "❌ $csproj_file not found!"
  exit 1
fi

# 🧪 Step 5: Create test file main.cs (optional)
echo "📄 Creating main.cs..."
touch main.cs
read -p "📝 Edit main.cs now? (Enter = yes, n = skip): " edit_main
[ "$edit_main" != "n" ] && vim main.cs

# ✏️ Step 6: Edit source file
read -p "📝 Edit $source_file now? (Enter = yes, n = skip): " edit_code
[ "$edit_code" != "n" ] && vim "$source_file"

# 🔨 Step 7: Build the project
echo "🔧 Building..."
dotnet build
[ $? -ne 0 ] && echo "❌ Build failed!" && exit 1

# 🚀 Step 8: Run project
echo "🚀 Running..."
dotnet run

# 📘 Step 9: Check for XML doc
xml_path="bin/Debug/netcoreapp2.1/${xml_output_name}"
if [ -f "$xml_path" ]; then
  echo "✅ XML documentation found at: $xml_path"
else
  echo "⚠️ XML doc not found. Check XML comments and csproj settings."
fi

# 📝 Step 10: Create root README.md if missing
if [ ! -f "../../README.md" ]; then
  echo "📘 Creating README.md at root..."
  echo "# holbertonschool-csharp - Delegates, Events" > ../../README.md
  echo "_Auto-generated README for Delegates project._" >> ../../README.md
fi

# 🌐 Step 11: Git push
read -p "🌍 Push to GitHub now? (yes/no): " push
if [ "$push" = "yes" ]; then
  cd ../..
  git add .
  git commit -m "Completed task: $task_name"
  git push
  echo "✅ Pushed to GitHub"
else
  echo "🔒 Skipped Git push. Push manually later."
fi

# 🎉 Final congrats
congratulate "$task_name"

