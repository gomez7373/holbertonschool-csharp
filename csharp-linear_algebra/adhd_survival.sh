#!/bin/bash

# Function to display a step completion message with a happy face
function congratulate() {
  echo "😊 Congratulations! The task '$1' has been successfully completed! 😊"
}

# Step 1: Create a new .NET console application
read -p "Enter the name for your task/project file: " task_name

# Execute the dotnet new console command
echo "Creating a new .NET console application..."
dotnet new console -n "$task_name"
cd "$task_name" || exit

# Step 2: Rename the default file to match the directory name
project_file="${task_name}.cs"
if [ -f "Program.cs" ]; then
  mv Program.cs "$project_file"
  echo "Renamed Program.cs to $project_file."
else
  echo "Error: Program.cs not found!"
  exit 1
fi

# Step 3: Create a main.cs file
main_file="main.cs"
echo "Creating main.cs..."
touch "$main_file"
read -p "Ready to edit main.cs? (Press Enter to continue)"
vim "$main_file"

# Step 4: Edit the renamed .cs file
read -p "Ready to edit $project_file? (Press Enter to continue)"
vim "$project_file"

# Step 5: Build the project
echo "Building the project..."
dotnet build
if [ $? -ne 0 ]; then
  echo "Build failed! Exiting..."
  exit 1
fi

# Step 6: Run the project
echo "Running the project..."
dotnet run

# Step 7: Delete specified files
files_to_delete=("obj" "bin" "$main_file")
echo "Deleting generated files..."
for file in "${files_to_delete[@]}"; do
  if [ -e "$file" ]; then
    rm -rf "$file"
    echo "Deleted $file."
  else
    echo "$file does not exist. Skipping..."
  fi
done

# Step 8: Edit the .csproj file content
csproj_file="${task_name}.csproj"
if [ -f "$csproj_file" ]; then
  echo "Opening the .csproj file for manual editing..."
  echo "Ensure it contains the following content:"
  echo "
<Project Sdk=\"Microsoft.NET.Sdk\">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp2.1</TargetFramework>
    <RootNamespace>_2_replace_element</RootNamespace>
  </PropertyGroup>

</Project>
"
  read -p "Press Enter to open the file with vim for editing..."
  vim "$csproj_file"
  echo "Make sure to save and exit vim after making the changes!"
else
  echo "Error: $csproj_file not found! Skipping manual edit."
fi

# Step 9: Congratulate the user
congratulate "$task_name"

# Step 10: Check for README.md and push to GitHub
read -p "Have you created a README.md file? (yes/no): " has_readme
if [ "$has_readme" = "yes" ]; then
  read -p "Ready to submit the code to GitHub? (yes/no): " ready_to_push
  if [ "$ready_to_push" = "yes" ]; then
    git add .
    git commit -m "task"
    git push
    echo "Code pushed to GitHub!"
  else
    echo "Skipping GitHub submission."
  fi
else
  echo "Please create a README.md file before submitting to GitHub."
fi


