#!/usr/bin/env bash
dotnet new console -o 1-new_project
dotnet restore 1-new_project
dotnet build 1-new_project
dotnet run --project ./1-new_project
