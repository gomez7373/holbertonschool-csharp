#!/bin/bash

# Base project directory
PROJECT_DIR="csharp-arrays_lists_dictionaries"

# Task names based on project description
declare -a TASK_NAMES=(
    "Print an array of integers"
    "Access an element in an array"
    "Replace element"
    "Print an array of integers in reverse"
    "Print a list of integers"
    "Find the max"
    "Only by 2"
    "Delete at"
    "Number of keys"
    "Update dictionary"
    "Delete from dictionary"
    "Multiply by 2"
    "Print sorted dictionary"
    "Best score"
    "Rectangular array"
    "Matrix squared"
)

# Create the base directory
echo "Creating base directory: $PROJECT_DIR"
mkdir -p "$PROJECT_DIR"

# Loop through tasks
for TASK_NUM in "${!TASK_NAMES[@]}"; do
  TASK_NAME="${TASK_NAMES[TASK_NUM]}"
  TASK_DIR="$PROJECT_DIR/$TASK_NUM-${TASK_NAME// /_}"  # Replace spaces with underscores
  MAIN_FILE="$TASK_DIR/Main.cs"

  echo "Creating directory: $TASK_DIR"
  mkdir -p "$TASK_DIR"

  echo "Creating Main.cs in $TASK_DIR"
  cat > "$MAIN_FILE" <<EOL
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Task $TASK_NUM: $TASK_NAME");
    }
}
EOL
done

echo "Directories and Main.cs files created for all tasks."

