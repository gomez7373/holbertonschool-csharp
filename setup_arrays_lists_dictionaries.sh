#!/bin/bash

# Function to create a task project
create_task_project() {
    local task_name=$1
    local task_code=$2
    local main_code=$3

    echo "Creating project for task: $task_name"

    # Create directory for the task
    mkdir -p "$task_name"
    cd "$task_name" || exit

    # Initialize a new console application
    dotnet new console

    # Rename Program.cs to the task-specific name
    mv Program.cs "$task_name.cs"

    # Write the implementation code
    echo "$task_code" >"$task_name.cs"

    # Create main.cs file
    echo "$main_code" >main.cs

    # Modify .csproj file for .NET Core 2.1 compatibility
    sed -i '/EnableDefaultCompileItems/d' "$task_name.csproj"
    sed -i '/EnableDefaultItems/d' "$task_name.csproj"
    sed -i 's/<TargetFramework>.*<\/TargetFramework>/<TargetFramework>netcoreapp2.1<\/TargetFramework>/g' "$task_name.csproj"

    # Build and run the project
    echo "Building the project for $task_name..."
    dotnet build
    echo "Running the project for $task_name..."
    dotnet run --project "$task_name.csproj"

    # Go back to the parent directory
    cd ..
}

# Task definitions: [Task Name, Task Code, Main Code]
declare -A tasks

tasks["0-print_array"]='
public static int[] CreatePrint(int size) {
    if (size < 0) {
        Console.WriteLine("Size cannot be negative");
        return null;
    }
    int[] array = new int[size];
    for (int i = 0; i < size; i++) {
        array[i] = i;
    }
    Console.WriteLine(string.Join(" ", array));
    return array;
}
'

tasks["1-element_at"]='
public static int elementAt(int[] array, int index) {
    if (index < 0 || index >= array.Length) {
        Console.WriteLine("Index out of range");
        return -1;
    }
    return array[index];
}
'

tasks["2-replace_element"]='
public static int[] ReplaceElement(int[] array, int index, int n) {
    if (index < 0 || index >= array.Length) {
        Console.WriteLine("Index out of range");
        return array;
    }
    array[index] = n;
    return array;
}
'

tasks["3-print_array_reverse"]='
public static void Reverse(int[] array) {
    if (array == null || array.Length == 0) {
        Console.WriteLine();
        return;
    }
    Array.Reverse(array);
    Console.WriteLine(string.Join(" ", array));
}
'

tasks["4-print_list"]='
using System.Collections.Generic;
public static List<int> CreatePrint(int size) {
    if (size < 0) {
        Console.WriteLine("Size cannot be negative");
        return null;
    }
    List<int> list = new List<int>();
    for (int i = 0; i < size; i++) {
        list.Add(i);
    }
    Console.WriteLine(string.Join(" ", list));
    return list;
}
'

tasks["5-max_integer"]='
using System.Collections.Generic;
public static int MaxInteger(List<int> myList) {
    if (myList.Count == 0) {
        Console.WriteLine("List is empty");
        return -1;
    }
    int max = int.MinValue;
    foreach (int num in myList) {
        if (num > max) max = num;
    }
    return max;
}
'

tasks["6-divisible_by_2"]='
using System.Collections.Generic;
public static List<bool> DivisibleBy2(List<int> myList) {
    List<bool> results = new List<bool>();
    foreach (int num in myList) {
        results.Add(num % 2 == 0);
    }
    return results;
}
'

tasks["7-delete_at"]='
using System.Collections.Generic;
public static List<int> DeleteAt(List<int> myList, int index) {
    if (index < 0 || index >= myList.Count) {
        Console.WriteLine("Index is out of range");
        return myList;
    }
    myList.RemoveAt(index);
    return myList;
}
'

tasks["8-number_keys"]='
using System.Collections.Generic;
public static int NumberOfKeys(Dictionary<string, string> myDict) {
    int count = 0;
    foreach (var key in myDict.Keys) {
        count++;
    }
    return count;
}
'

tasks["9-add_key_value"]='
using System.Collections.Generic;
public static Dictionary<string, string> AddKeyValue(Dictionary<string, string> myDict, string key, string value) {
    myDict[key] = value;
    return myDict;
}
'

tasks["10-delete_key_value"]='
using System.Collections.Generic;
public static Dictionary<string, string> DeleteKeyValue(Dictionary<string, string> myDict, string key) {
    myDict.Remove(key);
    return myDict;
}
'

tasks["11-multiply_by_2"]='
using System.Collections.Generic;
public static Dictionary<string, int> MultiplyBy2(Dictionary<string, int> myDict) {
    Dictionary<string, int> result = new Dictionary<string, int>();
    foreach (var pair in myDict) {
        result[pair.Key] = pair.Value * 2;
    }
    return result;
}
'

tasks["12-print_sorted_dictionary"]='
using System.Collections.Generic;
using System.Linq;
public static void PrintSorted(Dictionary<string, string> myDict) {
    foreach (var pair in myDict.OrderBy(x => x.Key)) {
        Console.WriteLine($"{pair.Key}: {pair.Value}");
    }
}
'

tasks["13-best_score"]='
using System.Collections.Generic;
public static string BestScore(Dictionary<string, int> myList) {
    if (myList.Count == 0) return "None";
    KeyValuePair<string, int> maxPair = myList.Aggregate((x, y) => x.Value > y.Value ? x : y);
    return maxPair.Key;
}
'

tasks["14-rectangular_array"]='
public static void PrintArray() {
    int[,] array = new int[5, 5];
    array[2, 2] = 1;
    for (int i = 0; i < 5; i++) {
        for (int j = 0; j < 5; j++) {
            Console.Write(array[i, j] + " ");
        }
        Console.WriteLine();
    }
}
'

tasks["15-square_matrix"]='
public static int[,] Square(int[,] myMatrix) {
    int rows = myMatrix.GetLength(0);
    int cols = myMatrix.GetLength(1);
    int[,] result = new int[rows, cols];
    for (int i = 0; i < rows; i++) {
        for (int j = 0; j < cols; j++) {
            result[i, j] = myMatrix[i, j] * myMatrix[i, j];
        }
    }
    return result;
}
'

# Main function to create all projects
main() {
    for task_name in "${!tasks[@]}"; do
        create_task_project "$task_name" "${tasks[$task_name]}" "using System; class Program { static void Main(string[] args) { // Add example code for $task_name } }"
    done
}

main

