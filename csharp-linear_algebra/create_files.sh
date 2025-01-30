#!/bin/bash

# Files that don't belong in specific directories
files=(
    "0-pythagoras"
    "1-magnitude_2D"
    "2-magnitude_3D"
    "4-vector_addition_2D"
    "5-vector_addition_3D"
    "7-vector_scalar_mul_2D"
    "8-vector_scalar_mul_3D"
    "10-dot_product_2D"
    "11-dot_product_3D"
    "13-matrix_addition"
    "15-matrix_scalar_mul"
    "17-matrix_matrix_mul"
    "19-matrix_rotate_2D"
    "23-matrix_shear_2D"
    "26-determinant_2D"
    "27-determinant_3D"
    "29-cross_product"
    "31-inverse_2D"
    "32-inverse_3D"
)

# Create files
for file in "${files[@]}"; do
    if [ ! -f "$file" ]; then
        touch "$file"
        echo "Created file: $file"
    else
        echo "File already exists: $file"
    fi
done

echo "All specified files have been created!"

