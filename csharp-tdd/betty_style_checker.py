import os
import re
import sys

def find_cs_files(directory):
    """Recursively find all .cs files in the directory."""
    cs_files = []
    for root, _, files in os.walk(directory):
        for file in files:
            if file.endswith(".cs"):
                cs_files.append(os.path.join(root, file))
    return cs_files

def analyze_and_fix_betty_style(file_path):
    """Analyze and fix Betty-style issues."""
    with open(file_path, 'r') as file:
        lines = file.readlines()

    fixed_lines = []
    issues = []

    for line_num, line in enumerate(lines, start=1):
        original_line = line

        # Rule: Use 4 spaces for indentation
        if line.startswith('\t') or (line.strip() and not line.startswith(' ' * 4) and not line.strip().startswith('//')):
            leading_spaces = len(line) - len(line.lstrip())
            fixed_line = (' ' * 4 * (leading_spaces // 4)) + line.lstrip()
            if fixed_line != line:
                issues.append(f"Line {line_num}: Incorrect indentation (use 4 spaces).")
                line = fixed_line

        # Rule: Allman brace style
        if '{' in line and not line.strip().startswith('{') and not '}' in line:
            issues.append(f"Line {line_num}: Opening brace '{{' should be on a new line.")
            line = line.replace('{', '').rstrip() + '\n' + ' ' * 4 + '{'

        fixed_lines.append(line)

    if issues:
        print(f"❌ Issues found in '{file_path}':")
        for issue in issues:
            print(f"  - {issue}")
        fix = input(f"Do you want to fix these issues in '{file_path}'? (yes/no): ").strip().lower()
        if fix == 'yes':
            with open(file_path, 'w') as file:
                file.writelines(fixed_lines)
            print(f"✨ Congrats! '{file_path}' is now Betty-style compliant! 🎉")
        else:
            print(f"Skipped fixing '{file_path}'.")
    else:
        print(f"✅ '{file_path}' is already Betty-style compliant! 🎉")

def main():
    if len(sys.argv) != 2:
        print("Usage: python3 betty_style_checker.py <directory>")
        sys.exit(1)

    directory = sys.argv[1]

    if not os.path.isdir(directory):
        print(f"Error: '{directory}' is not a valid directory.")
        sys.exit(1)

    cs_files = find_cs_files(directory)
    if not cs_files:
        print("No .cs files found in the specified directory.")
        return

    print(f"Found {len(cs_files)} .cs files. Analyzing...\n")
    for cs_file in cs_files:
        print(f"Analyzing '{cs_file}'...")
        analyze_and_fix_betty_style(cs_file)

if __name__ == "__main__":
    main()

