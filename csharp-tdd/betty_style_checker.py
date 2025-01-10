import re
import sys
import os

def find_cs_files(directory):
    """Recursively finds all .cs files in the given directory."""
    cs_files = []
    for root, _, files in os.walk(directory):
        for file in files:
            if file.endswith('.cs'):
                cs_files.append(os.path.join(root, file))
    return cs_files

def analyze_betty_style(file_path):
    """Analyzes the C# code in the provided file for Betty-style violations."""
    with open(file_path, 'r') as file:
        lines = file.readlines()

    issues = []
    
    # Rule 1: Check for PascalCase class names
    class_regex = r'^\s*public\s+class\s+([a-z][a-zA-Z0-9]*)'
    for line_num, line in enumerate(lines, start=1):
        match = re.search(class_regex, line)
        if match:
            issues.append(
                f"Line {line_num}: Class name '{match.group(1)}' is not in PascalCase."
            )

    # Rule 2: Check for 4-space indentation
    for line_num, line in enumerate(lines, start=1):
        if not line.startswith((' ' * 4)) and line.strip() and not line.strip().startswith('//'):
            issues.append(f"Line {line_num}: Incorrect indentation (use 4 spaces).")
    
    # Rule 3: Check for Allman brace style
    brace_regex = r'{\s*\n'
    for line_num, line in enumerate(lines, start=1):
        if '{' in line and not re.search(brace_regex, line):
            issues.append(f"Line {line_num}: Opening brace '{{' should be on a new line.")

    return issues, lines

def fix_betty_style(file_path, lines):
    """Fixes Betty-style issues in the code and rewrites the file."""
    for i, line in enumerate(lines):
        # Fix indentation
        lines[i] = line.replace('\t', ' ' * 4)
        
        # Fix Allman brace style
        if '{' in line and not line.strip().startswith('{') and '}' not in line:
            lines[i] = line.replace('{', '') + '\n' + ' ' * 4 + '{'

    # Write fixed code to the file
    with open(file_path, 'w') as file:
        file.writelines(lines)

    print(f"✨ Congrats! '{file_path}' is now Betty-style compliant! 🎉")

def main():
    if len(sys.argv) != 2:
        print("Usage: python betty_style_checker.py <directory_path>")
        sys.exit(1)

    directory = sys.argv[1]

    if not os.path.isdir(directory):
        print(f"Error: '{directory}' is not a valid directory.")
        sys.exit(1)

    cs_files = find_cs_files(directory)

    if not cs_files:
        print("No .cs files found in the specified directory.")
        return

    print(f"Found {len(cs_files)} .cs files. Analyzing...")

    for cs_file in cs_files:
        print(f"\nAnalyzing '{cs_file}'...")
        issues, lines = analyze_betty_style(cs_file)

        if not issues:
            print(f"✅ '{cs_file}' is already Betty-style compliant! 🎉")
        else:
            print(f"❌ Issues found in '{cs_file}':")
            for issue in issues:
                print(f"  - {issue}")

            fix = input(f"Do you want to fix these issues in '{cs_file}'? (yes/no): ").strip().lower()
            if fix == 'yes':
                fix_betty_style(cs_file, lines)

if __name__ == "__main__":
    main()

