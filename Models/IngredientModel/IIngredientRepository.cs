using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SueRecipes.Models.IngredientModel
{
    public interface IIngredientRepository
    {
        IEnumerable<Ingredient> GetAllIngredients();
        Ingredient GetIngredient(int Id);
        Ingredient AddIngredient(Ingredient newIngredient);
        Ingredient EditIngredient(Ingredient editedIngredient);
        Ingredient DeleteIngredient(int Id);
    }
}
