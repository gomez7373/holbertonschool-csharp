
Curriculum
[C#24] Spe - AR/VR 2025
Average: 79.5%
You have a captain's log due before 2025-07-27 (in about 23 hours)! Log it now!
Project badge
C# - Text-based Interface
 Amateur
 By: Holberton School
 Weight: 1
 Your score will be updated as you progress.
 Manual QA review must be done (request it when you are done with the project)
Description
In this project, we’ll be using all of the collective Foundations and C# skills you’ve gained to create a console application that allows a user to manage inventories of items.

Data will be stored in JSON format
Users must be able to create, read, update, and delete objects
The class and attribute names used in this application will be generic, making it versatile for different use cases and easily extended
This project is more flexible than previous console application projects. The checker will not be looking for specific output; instead, the majority of your application will be manually reviewed. This means that you will not be given specifics on exactly how things should be printed or how input is taken. This reflects how you may need to approach projects as a software engineer in the industry. You won’t always be given specifications to follow to the letter – you’ll need to be able to make your own informed, logical decisions about an application’s functionality. Some key things you should consider when creating the console application:

How should the application take properties for a new or updated object? Should it be done in one line? Or should there be a secondary prompt after their initial create/update command? How else can this be implemented?
What’s a readable way to display an object in the terminal?
Should there be a confirmation prompt for deleting objects?
What commands require error messages? What should an error message contain in order to be useful?
What should be done to make your Github repo informative and useful for other contributors and users of this application?
Resources
Read or watch:

System.Console
System.IO
System.Text.Json
Requirements
C# Tasks
Allowed editors: Visual Studio Code
All files will be compiled on Ubuntu 20.04 LTS using dotnet
A README.md file, at the root of the folder of the project, is mandatory
All default C# files named Program.cs should be renamed to the name given in each task
You do not need to push your obj/ or bin/ folders
All your public classes and their members should have XML documentation tags
All your private classes and members should be documented but without XML documentation tags
C# Tests
Allowed editors: Visual Studio Code
All tests should be inside a separate folder
All your test files will be executed using dotnet test
We strongly encourage you to work together on tests so that you don’t miss any edge cases
For this project:
Based on the requirements of each task, you should write the documentation and tests first before you actually code anything
The intranet checks for C# projects won’t be released before their first deadline in order for you to focus more on TDD and thinking about all possible cases
We strongly encourage you to work together on tests so that you don’t miss any edge cases
Don’t trust the user, always think about all possible edge cases
Tasks
0. Base knowledge
mandatory
Create a new solution called InventoryManagement. In the solution, add a new .NET class library project called InventoryLibrary.

Within the InventoryLibrary, create a class called BaseClass that all other classes will inherit from. BaseClass should define:

Properties:
id - string
date_created - DateTime
date_updated - DateTime
Repo:

GitHub repository: holbertonschool-csharp
Directory: csharp-text_based_interface
File: InventoryManagement.sln, InventoryLibrary/, InventoryLibrary/BaseClass.cs
 
0/0 pts
1. Taking inventory
mandatory
Within the InventoryLibrary, create a class called Item that inherits from BaseClass. Item should define:

Required properties
name - string
Optional properties
description - string
price - float, limit to 2 decimal points
tags - a list of strings
Within the InventoryLibrary, create a class called User that inherits from BaseClass. User should define:

Required properties
name - string
Within the InventoryLibrary, create a class called Inventory that inherits from BaseClass. Inventory should define:

Required properties
user_id - from User id
item_id - from Item id
quantity - int, default value: 1, cannot be less than 0
A required property must have a value on creation, it cannot be left blank

Repo:

GitHub repository: holbertonschool-csharp
Directory: csharp-text_based_interface
File: InventoryLibrary/Item.cs, InventoryLibrary/User.cs, InventoryLibrary/Inventory.cs
 
0/0 pts
2. In Store
mandatory
With our classes defined, let’s create our storage class. Create a class called JSONStorage. JSONStorage should define:

Properties:
objects - dictionary where keys are <ClassName>.<id> and values are the objects
Methods:
All() - return objects dictionary
New(obj) - add a new key-value pair to objects
Key: <obj ClassName>.<obj id>
Value: obj
Save() - serializes objects to JSON and saves to the JSON file
Load() - deserializes JSON file to objects
Use the built-in JSON namespace for serialization/deserialization.

The JSON file should be located in a directory named storage and the file named inventory_manager.json.

Repo:

GitHub repository: holbertonschool-csharp
Directory: csharp-text_based_interface
File: InventoryLibrary/JSONStorage.cs, storage/, storage/inventory_manager.json
 
0/0 pts
3. Inventory Manager Simulator
mandatory
Time to create the console application! Add a new console application called InventoryManager to the solution and add a reference to the InventoryLibrary.

The console application should:

On start, load all objects from JSONStorage
There should only be one instance of JSONStorage during the runtime of the console application
Print an initial prompt with the available commands and their usage. Example:
Inventory Manager
-------------------------
<ClassNames> show all ClassNames of objects
<All> show all objects
<All [ClassName]> show all objects of a ClassName
<Create [ClassName]> a new object
<Show [ClassName object_id]> an object
<Update [ClassName object_id]> an object
<Delete [ClassName object_id]> an object
<Exit>
Take user input from the command line using the Console namespace
ClassNames
Print all class names of objects loaded in JSONStorage
All
Print all objects
All <ClassName>
Print all objects of the given ClassName
Create <ClassName>
Create and save a new object of ClassName
Show <ClassName> <id>
Print the string representation of the requested object
Update <ClassName> <id>
Update the properties of the given object
Delete <ClassName> <id>
Delete the given object from the JSONStorage
Exit
Quit the application
For all commands that take options, if the user input is invalid, print the following errors:
If the given ClassName is invalid, print:
<ClassName> is not a valid object type
If the given id is invalid, print:
Object <id> could not be found
Input should not be case sensitive
When a command is completed without error, print the initial prompt with command list again
Repo:

GitHub repository: holbertonschool-csharp
Directory: csharp-text_based_interface
File: InventoryManager/, InventoryManager/InventoryManager.cs
0/33 pts
4. Test 1, 2, 3
mandatory
Your project must have unit tests! The number of tests is less important than the quality and effectiveness of your tests. Make sure you’re checking your object operations, edge cases, all user input possibilities, etc.

Repo:

GitHub repository: holbertonschool-csharp
Directory: csharp-text_based_interface
File: InventoryManagement.Tests/
 
0/6 pts
Score
Project badge
Your score will be updated as you progress.

Please review all the tasks before you start the peer review.

Welcome to the chat! Type a message to get started.

