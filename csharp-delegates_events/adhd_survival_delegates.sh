#!/bin/bash

# Function to display a step completion message
function congratulate() {
  echo "✅ Well done! The task '$1' was created and compiled successfully!"
}

# Step 1: Prompt for task/project name
read -p "📁 Enter task folder name (e.g., 0-universal_health): " task_name

# Step 2: Create new console project
echo "📦 Creating .NET console app in '$task_name'..."
dotnet new console -n "$task_name"
cd "$task_name" || exit 1

# Step 3: Rename Program.cs to match project name (e.g., 0-universal_health.cs)
project_file="${task_name}.cs"
if [ -f "Program.cs" ]; then
  mv Program.cs "$project_file"
  echo "✅ Renamed Program.cs to $project_file"
else
  echo "❌ Error: Program.cs not found!"
  exit 1
fi

# Step 4: Modify .csproj file automatically
csproj_file="${task_name}.csproj"
if [ -f "$csproj_file" ]; then
  echo "🛠️ Updating $csproj_file..."

  xml_output_name="${task_name}.xml"

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
  echo "❌ Error: $csproj_file not found!"
  exit 1
fi

# Step 5: Create optional main.cs (for testing if needed)
main_file="main.cs"
echo "📄 Creating $main_file (optional)..."
touch "$main_file"
read -p "📝 Edit $main_file now? (Enter = yes, n = skip): " edit_main
if [ "$edit_main" != "n" ]; then
  vim "$main_file"
fi

# Step 6: Edit main source file (renamed .cs file)
read -p "📝 Edit $project_file now? (Enter = yes, n = skip): " edit_code
if [ "$edit_code" != "n" ]; then
  vim "$project_file"
fi

# Step 7: Build the project
echo "🔧 Building..."
dotnet build
if [ $? -ne 0 ]; then
  echo "❌ Build failed. Fix errors and try again."
  exit 1
fi

# Step 8: Run the project
echo "🚀 Running..."
dotnet run

# Step 9: Confirm XML file exists
xml_path="bin/Debug/netcoreapp2.1/${xml_output_name}"
if [ -f "$xml_path" ]; then
  echo "✅ XML documentation found at: $xml_path"
else
  echo "⚠️ XML file not generated. Check XML comments or csproj settings."
fi

# Step 10: Create README.md if missing
if [ ! -f "../README.md" ]; then
  echo "📘 Creating README.md..."
  echo "# ${task_name}" > ../README.md
  echo "_Auto-generated README for Holberton C# Delegates, Events project._" >> ../README.md
fi

# Step 11: Ask for Git push
read -p "🌍 Push to GitHub now? (yes/no): " push
if [ "$push" = "yes" ]; then
  cd ..
  git add .
  git commit -m "Completed task: $task_name"
  git push
  echo "✅ Pushed to GitHub"
else
  echo "🔒 Skipped Git push. Don’t forget to commit manually."
fi

# Final message
congratulate "$task_name"

