#!/bin/bash

# Check for required argument
if [ $# -ne 2 ]; then
    echo "Usage: $0 <task_number> <namespace>"
    echo "Example: $0 0-add MyMath"
    exit 1
fi

TASK_NUMBER=$1
NAMESPACE=$2

# Step 1: Create the task directory and navigate into it
mkdir "$TASK_NUMBER" && cd "$TASK_NUMBER"

# Step 2: Create the solution file
dotnet new sln -n "$TASK_NUMBER"

# Step 3: Create the class library
mkdir "$NAMESPACE" && cd "$NAMESPACE"
dotnet new classlib
mv Class1.cs "$NAMESPACE.cs"

# Enable XML documentation
sed -i '/<\/PropertyGroup>/i \ \ \ \ <DocumentationFile>bin\\$(Configuration)\\$(TargetFramework)\\$(AssemblyName).xml</DocumentationFile>' "$NAMESPACE.csproj"
cd ..

# Step 4: Add the class library to the solution
dotnet sln add "$NAMESPACE/$NAMESPACE.csproj"

# Step 5: Create the test library
TEST_NAMESPACE="${NAMESPACE}.Tests"
mkdir "$TEST_NAMESPACE" && cd "$TEST_NAMESPACE"
dotnet new nunit
mv UnitTest1.cs "${NAMESPACE}.Tests.cs"

# Add reference to the class library
dotnet add reference "../$NAMESPACE/$NAMESPACE.csproj"
cd ..

# Step 6: Add the test library to the solution
dotnet sln add "$TEST_NAMESPACE/$TEST_NAMESPACE.csproj"

# Step 7: Final Output
echo "Task $TASK_NUMBER setup complete!"
echo "Directories and files created:"
tree -L 2

# Instructions for user
echo ""
echo "Next steps:"
echo "1. Implement your method in $NAMESPACE/$NAMESPACE.cs."
echo "2. Write your tests in $TEST_NAMESPACE/${NAMESPACE}.Tests.cs."
echo "3. Run tests with: dotnet test"

