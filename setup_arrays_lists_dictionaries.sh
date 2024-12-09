#!/bin/bash

# Ensure dotnet is installed
if ! command -v dotnet &> /dev/null; then
    echo "dotnet command not found. Please install .NET SDK."
    exit 1
fi

# Project directory setup
project_dir="csharp-arrays_lists_dictionaries"
mkdir -p $project_dir
cd $project_dir || exit

# Array of task descriptions and their corresponding codes
declare -A tasks=(
    [0]="Array.CreatePrint(10);"
    [1]="int[] array = {10, 17, -8, 4, -12, 7, 0, 1, -1, -9};\n        Console.WriteLine(\"Element at index 4 is {0}\", Array.elementAt(array, 4));"
    [2]="int[] array = {0, 1, 2, 3, 4, 5, 6};\n        Array.ReplaceElement(array, 1, 98);"
    [3]="int[] array = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};\n        Array.Reverse(array);"
    [4]="List.CreatePrint(10);"
    [5]="List<int> myList = new List<int>() {98, -10, 0, 32, 972, 12, 1024};\n        Console.WriteLine(\"Max: \" + List.MaxInteger(myList));"
    [6]="List<int> myList = new List<int>() {1, 2, 3, 4, 5};\n        List.DivisibleBy2(myList);"
    [7]="List<int> myList = new List<int>() {1, 2, 3, 4, 5};\n        List.DeleteAt(myList, 2);"
    [8]="Dictionary<string, string> myDict = new Dictionary<string, string>();\n        myDict.Add(\"language\", \"C\");\n        Console.WriteLine(\"Keys: \" + Dictionary.NumberOfKeys(myDict));"
    [9]="Dictionary<string, string> myDict = new Dictionary<string, string>();\n        Dictionary.AddKeyValue(myDict, \"school\", \"Holberton\");"
    [10]="Dictionary<string, string> myDict = new Dictionary<string, string>();\n        Dictionary.DeleteKeyValue(myDict, \"school\");"
    [11]="Dictionary<string, int> myDict = new Dictionary<string, int>();\n        myDict.Add(\"John\", 12);\n        Dictionary.MultiplyBy2(myDict);"
    [12]="Dictionary<string, string> myDict = new Dictionary<string, string>();\n        Dictionary.PrintSorted(myDict);"
    [13]="Dictionary<string, int> myDict = new Dictionary<string, int>();\n        Console.WriteLine(\"Best Score: \" + Dictionary.BestScore(myDict));"
    [14]="Matrix.CreateMatrix();"
    [15]="int[,] myMatrix = new int[,] { {0, 1, 2}, {3, 4, 5}, {6, 7, 8} };\n        Matrix.Square(myMatrix);"
)

# Loop through tasks
for task_id in "${!tasks[@]}"; do
    task_dir="${task_id}-task"
    mkdir -p $task_dir
    cd $task_dir || exit

    # Initialize .NET project
    dotnet new console -n "${task_dir}"
    mv "${task_dir}/Program.cs" "${task_id}-task.cs"
    rm -rf "${task_dir}"

    # Insert task-specific code into .cs file
    cat > "${task_id}-task.cs" <<EOL
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        ${tasks[$task_id]}
    }
}
EOL

    # Modify .csproj for compatibility
    sed -i '/ImplicitUsings/d' "${task_id}-task.csproj"
    sed -i '/Nullable/d' "${task_id}-task.csproj"
    sed -i 's/net7.0/net8.0/' "${task_id}-task.csproj"

    # Build and run the project
    echo "Building and running task ${task_id}..."
    dotnet build
    dotnet run

    # Return to project directory
    cd ..
done

# Clean up bin and obj folders
find . -type d \( -name bin -o -name obj \) -exec rm -rf {} +

# Add and commit changes to git
echo "Committing changes to GitHub..."
git add .
git commit -m "Added all tasks for arrays, lists, and dictionaries project."
git push

echo "All tasks completed and committed successfully!"

