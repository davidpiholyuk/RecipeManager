using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace assignment.Function
{
    /// <summary>
    /// RecipeFunctions class includes the Azure Functions for managing recipes in the application.
    /// It contains methods for adding, updating, retrieving, and marking recipes as favorite.
    /// Author: David Piholyuk
    /// Date: 07/23/2023
    /// </summary>
    public class RecipeFunctions
    {
        private readonly ILogger _logger;
        private static Dictionary<string, Recipe> _recipes = new Dictionary<string, Recipe>();

        public RecipeFunctions(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<RecipeFunctions>();
        }

        /// <summary>
        /// This function is used to add a new recipe.
        /// </summary>
        [Function("AddRecipe")]
        public async Task<HttpResponseData> AddRecipe(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("AddRecipe");
            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var recipe = JsonSerializer.Deserialize<Recipe>(requestBody);

                if (recipe != null && !string.IsNullOrEmpty(recipe.Name)) 
                {
                    var name = recipe.Name;
                    _recipes.Add(name, recipe);
                }

                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(recipe);
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while adding the recipe.");
                var response = req.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        /// <summary>
        /// This function is used to update an existing recipe.
        /// </summary>
        [Function("UpdateRecipe")]
        public async Task<HttpResponseData> UpdateRecipe(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "recipes/{name}")] HttpRequestData req,
            string name,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("UpdateRecipe");
            try
            {
                if (!_recipes.ContainsKey(name))
                {
                    var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
                    return notFoundResponse;
                }

                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var updatedRecipe = JsonSerializer.Deserialize<Recipe>(requestBody);
                if (updatedRecipe != null) 
                {
                    var recipe = _recipes[name];
                    recipe.Name = updatedRecipe.Name;
                    _recipes.Remove(name);
                    _recipes.Add(recipe.Name, recipe);
                }

                var okResponse = req.CreateResponse(HttpStatusCode.OK);
                await okResponse.WriteAsJsonAsync(updatedRecipe);
                return okResponse;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while updating the recipe.");
                var response = req.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        /// <summary>
        /// This function is used to retrieve a specific recipe by name.
        /// </summary>
        [Function("GetRecipe")]
        public HttpResponseData GetRecipe(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "recipes/{name}")] HttpRequestData req,
            string name,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("GetRecipe");
            try
            {
                if (!_recipes.ContainsKey(name))
                {
                    var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
                    return notFoundResponse;
                }

                var recipe = _recipes[name];
                var okResponse = req.CreateResponse(HttpStatusCode.OK);
                okResponse.WriteAsJsonAsync(recipe);
                return okResponse;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while getting the recipe.");
                var response = req.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        /// <summary>
        /// This function is used to mark a recipe as favorite.
        /// </summary>
        [Function("FavoriteRecipe")]
        public async Task<HttpResponseData> FavoriteRecipe(
            [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "recipes/{name}/favorite")] HttpRequestData req,
            string name,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("FavoriteRecipe");
            try
            {
                if (!_recipes.ContainsKey(name))
                {
                    var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
                    return notFoundResponse;
                }

                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonSerializer.Deserialize<Recipe>(requestBody);
                var recipe = _recipes[name];
                if (data != null) 
                {
                    var favorite = data.Favorite;
                    recipe.Favorite = favorite;
                    _recipes[name] = recipe;
                }

                var okResponse = req.CreateResponse(HttpStatusCode.OK);
                await okResponse.WriteAsJsonAsync(recipe);
                return okResponse;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while favoriting the recipe.");
                var response = req.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        /// <summary>
        /// Class representing a recipe.
        /// </summary>
        [JsonSerializable(typeof(Recipe))]
        public class Recipe
        {
            public string Name { get; set; }
            public string Ingredients { get; set; }
            public string Instructions { get; set; }
            public string Favorite { get; set; }
        }
    }
}
