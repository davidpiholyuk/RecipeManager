# Recipe Management System

Description

This application serves as a recipe management system. It provides functionalities to add, retrieve, update, and mark recipes as favorite. It's built using Azure Functions for the backend and HTML, CSS and JavaScript for the frontend.

Pre-Requisites

    .NET Core 3.1 SDK or later
    Azure Functions Core Tools version 3.x or later
    Node.js version 10.x or later
    A code editor such as Visual Studio Code

Installation & Running the Application

    Clone the repository:

bash

git clone https://github.com/davidpiholyuk/RecipeManager.git

    Navigate to the project directory:

bash

cd repo-name

    Start the Azure Function:

wasm

func start

    Open the HTML file in a browser.

    Now you should be able to add, retrieve, update, and mark recipes as favorite using the form on the page.

Project Structure

The main backend functionalities are implemented in RecipeFunctions.cs file. It includes Azure Functions for managing recipes:

    AddRecipe: adds a new recipe to the system
    UpdateRecipe: updates an existing recipe in the system
    GetRecipe: retrieves a specific recipe from the system
    FavoriteRecipe: marks a recipe as favorite

The frontend is a simple HTML page with forms for each operation.

Note: Be sure to replace "username" and "repo-name" in the git clone command with your actual GitHub username and repository name. You also might need to update the pre-requisites, installation steps, and project structure based on your actual project setup.
