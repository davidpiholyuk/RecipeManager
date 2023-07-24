# Recipe Management System

## Description

This application serves as a recipe management system. It provides functionalities to add, retrieve, update, and mark recipes as favorites. It's built using Azure Functions for the backend and HTML, CSS, and JavaScript for the frontend.

## Pre-Requisites

1. [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download) or later
2. [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=windows%2Ccsharp%2Cbash#v2) version 3.x or later
3. [Node.js](https://nodejs.org/en/) version 10.x or later
4. A code editor such as [Visual Studio Code](https://code.visualstudio.com/)

## Installation & Running the Application

1. Clone the repository:

2. Navigate to the project directory:

3. Start the Azure Function:

   Run: ```func start```

4. Open the HTML file inside of the frontend folder in a browser.

5. Now you should be able to add, retrieve, update, and mark recipes as favorites using the form on the page. 

## Project Structure

The main backend functionalities are implemented in the `RecipeFunctions.cs` file. It includes Azure Functions for managing recipes:

- AddRecipe: adds a new recipe to the system
- UpdateRecipe: updates an existing recipe in the system
- GetRecipe: retrieves a specific recipe from the system
- FavoriteRecipe: marks a recipe as favorite

The front end is a simple HTML page with forms for each operation.


