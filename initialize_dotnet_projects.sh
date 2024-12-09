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

# Loop through tasks
for TASK_NUM in "${!TASK_NAMES[@]}"; do
  TASK_NAME="${TASK_NAMES[TASK_NUM]}"
  TASK_DIR="$PROJECT_DIR/$TASK_NUM-${TASK_NAME// /_}"  # Replace spaces with underscores

  if [ -d "$TASK_DIR" ]; then
    echo "Initializing .NET console project in $TASK_DIR"
    (cd "$TASK_DIR" && dotnet new console > /dev/null)
  else
    echo "Directory $TASK_DIR does not exist. Skipping."
  fi
done

echo "dotnet new console initialized for all task directories."

