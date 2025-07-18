#!/bin/bash

echo "🧹 Limpiando el proyecto..."
dotnet clean

echo "🧽 Eliminando manualmente bin/ y obj/..."
rm -rf bin/ obj/

echo "🔄 Restaurando dependencias..."
dotnet restore

echo "🔧 Compilando proyecto..."
dotnet build

echo "🚀 Ejecutando el proyecto..."
dotnet run

