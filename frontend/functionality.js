// The event listener ensures the script runs after the DOM has fully loaded
document.addEventListener('DOMContentLoaded', function() {
    
    // Event listener for the form to add a new recipe
    document.getElementById('add-recipe-form').addEventListener('submit', function(event) {
        // Prevents the default form submission
        event.preventDefault();

        // Retrieves the values from the input fields
        var name = document.getElementById('recipe-name').value;
        var ingredients = document.getElementById('recipe-ingredients').value;
        var instructions = document.getElementById('recipe-instructions').value;

        // POST request to add a new recipe
        fetch(`http://localhost:7071/api/AddRecipe`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                Name: name,
                Ingredients: ingredients,
                Instructions: instructions,
                Favorite: "No"
            })
        })
        .then(response => response.json())
        .then(data => { alert('Recipe added successfully!'); });
    });

    // Event listener for the form to retrieve a recipe
    document.getElementById('get-recipe-form').addEventListener('submit', function(event) {
        event.preventDefault();

        var name = document.getElementById('get-recipe-name').value;

        // GET request to retrieve a specific recipe
        fetch(`http://localhost:7071/api/recipes/${name}`)
            .then(response => response.json())
            .then(data => {
                // Displaying the recipe details
                document.getElementById('recipe-details').innerHTML = `
                    <h2>Name: ${data.Name}</h2>
                    <p>Ingredients: ${data.Ingredients}</p>
                    <p>Instructions: ${data.Instructions}</p>
                    <p>Is this recipe a favorite: ${data.Favorite}</p>
                `;
        });
    });

    // Event listener for the form to update a recipe
    document.getElementById('update-recipe-form').addEventListener('submit', function(event) {
        event.preventDefault();

        var name = document.getElementById('update-recipe-name').value;
        var newName = document.getElementById('update-recipe-newname').value;

        // PUT request to update a specific recipe
        fetch(`http://localhost:7071/api/recipes/${name}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ Name: newName })
        })
        .then(response => response.json())
        .then(data => { alert('Recipe updated successfully!'); });
    });

    // Event listener for the form to mark a recipe as favorite
    document.getElementById('favorite-recipe-form').addEventListener('submit', function(event) {
        event.preventDefault();

        var name = document.getElementById('favorite-recipe-name').value;

        // PATCH request to update a specific field (Favorite) in a specific recipe
        fetch(`http://localhost:7071/api/recipes/${name}/favorite`, {
            method: 'PATCH',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ Favorite: "Yes" })
        })
        .then(response => response.json())
        .then(data => { alert('Recipe favorited successfully!'); });
    });
});
