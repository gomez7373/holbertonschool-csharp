#!/bin/bash

# Function to display a step completion message
function congratulate() {
  echo "😊 Congratulations! The task '$1' has been successfully completed! 😊"
}

# Step 1: Prompt for task/project name
read -p "Enter the name for your task/project folder: " task_name

# Step 2: Create new console project
echo "📦 Creating .NET console app in '$task_name'..."
dotnet new console -n "$task_name"
cd "$task_name" || exit 1

# Step 3: Rename Program.cs to match project name
project_file="queue.cs"
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
  echo "🛠️ Updating $csproj_file with Holberton requirements..."

  # Get correct XML file name from the .csproj file name
  xml_output_name="${csproj_file%.csproj}.xml"

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

  echo "✅ .csproj configured successfully."
else
  echo "❌ Error: $csproj_file not found!"
  exit 1
fi

# Step 5: Create a main.cs file for testing (optional)
main_file="main.cs"
echo "📄 Creating main.cs (optional test file)..."
touch "$main_file"
read -p "Edit main.cs now? (Enter = yes, n = skip): " edit_main
if [ "$edit_main" != "n" ]; then
  vim "$main_file"
fi

# Step 6: Edit the renamed source file
read -p "Edit $project_file now? (Enter = yes, n = skip): " edit_code
if [ "$edit_code" != "n" ]; then
  vim "$project_file"
fi

# Step 7: Build the project
echo "🔧 Building the project..."
dotnet build
if [ $? -ne 0 ]; then
  echo "❌ Build failed. Try fixing errors and re-running."
  exit 1
fi

# Step 8: Run the project
echo "🚀 Running the project..."
dotnet run

# Step 9: Ensure XML file exists
xml_path="bin/Debug/netcoreapp2.1/${xml_output_name}"
if [ -f "$xml_path" ]; then
  echo "✅ XML documentation generated at $xml_path"
else
  echo "⚠️ Warning: XML file was not found. Check your XML comments and csproj settings."
fi

# Step 10: Create README.md if missing
if [ ! -f "../README.md" ]; then
  echo "📘 Creating default README.md..."
  echo "# ${task_name}" > ../README.md
  echo "_This is an auto-generated README for Holberton C# Generics project._" >> ../README.md
fi

# Step 11: Ask for Git push
read -p "Do you want to push to GitHub now? (yes/no): " push
if [ "$push" = "yes" ]; then
  cd ..
  git add .
  git commit -m "Completed task: $task_name"
  git push
  echo "✅ Code pushed to GitHub."
else
  echo "🔒 Skipped Git push. Remember to push later."
fi

# Final message
congratulate "$task_name"
