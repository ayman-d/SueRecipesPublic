using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SueRecipes.Models.RecipeModel
{
    public interface IRecipeRepository 
    {
        IEnumerable<Recipe> GetAllRecipes();
        Recipe GetRecipe(int Id);
        Recipe AddRecipe(Recipe newRecipe);
        Recipe EditRecipe(Recipe editedRecipe);
        Recipe DeleteRecipe(int Id);
    }
}
