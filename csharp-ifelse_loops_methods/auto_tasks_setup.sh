#!/bin/bash

# Declare an associative array of remaining tasks with their names
declare -A tasks=(
    [9]="add"
    [10]="print_line"
    [11]="print_diagonal"
    [12]="fizzbuzz"
    # Add other tasks here as needed
)

# Task-specific code snippets
declare -A task_code=(
    [9]="using System;

class Number
{
    public static int Add(int a, int b)
    {
        return a + b;
    }
}"
    [10]="using System;

class Line
{
    public static void PrintLine(int length)
    {
        if (length <= 0)
        {
            Console.WriteLine();
            return;
        }
        Console.WriteLine(new string('_', length));
    }
}"
    [11]="using System;

class Line
{
    public static void PrintDiagonal(int length)
    {
        if (length <= 0)
        {
            Console.WriteLine();
            return;
        }
        for (int i = 0; i < length; i++)
        {
            Console.WriteLine(new string(' ', i) + \"\\\\\");
        }
    }
}"
    [12]="using System;

class Program
{
    public static void Main(string[] args)
    {
        for (int i = 1; i <= 100; i++)
        {
            if (i % 3 == 0 && i % 5 == 0)
                Console.Write(\"FizzBuzz \");
            else if (i % 3 == 0)
                Console.Write(\"Fizz \");
            else if (i % 5 == 0)
                Console.Write(\"Buzz \");
            else
                Console.Write(i + \" \");
        }
        Console.WriteLine();
    }
}"
)

# Iterate over each task in the array
for task_num in "${!tasks[@]}"
do
    task_name="${tasks[$task_num]}"
    dir_name="${task_num}-${task_name}"

    echo "Setting up task $task_num: $task_name"

    # Step 1: Create the directory and navigate into it
    mkdir -p $dir_name
    cd $dir_name || { echo "Failed to enter directory $dir_name"; exit 1; }

    # Step 2: Initialize a new console project
    dotnet new console

    # Step 3: Rename Program.cs to the task-specific name
    mv Program.cs "${task_num}-${task_name}.cs"

    # Step 4: Replace content in the .cs file with the task-specific code
    echo "${task_code[$task_num]}" > "${task_num}-${task_name}.cs"

    # Step 5: Build the project
    dotnet build

    # Step 6: Run the project to confirm it works
    dotnet run

    # Step 7: Modify the .csproj file for intranet recognition
    cs_proj_file="${task_num}-${task_name}.csproj"

    sed -i '/ImplicitUsings/d' $cs_proj_file
    sed -i '/Nullable/d' $cs_proj_file

    # Replace TargetFramework to match intranet requirements
    sed -i 's|<TargetFramework>.*</TargetFramework>|<TargetFramework>netcoreapp2.1</TargetFramework>|g' $cs_proj_file

    # Step 8: Push to GitHub
    git add .
    git commit -m "Task $task_num: $task_name"
    git push

    # Navigate back to the parent directory for the next task
    cd ..

    echo "Task $task_num: $task_name setup completed!"
done

echo "All tasks setup completed!"

