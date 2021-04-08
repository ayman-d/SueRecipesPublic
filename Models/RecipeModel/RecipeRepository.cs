using Microsoft.EntityFrameworkCore;
using SueRecipes.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SueRecipes.Models.RecipeModel
{
    public class RecipeRepository : IRecipeRepository
    {

        private readonly AppDbContext _context;

        public RecipeRepository(AppDbContext context)
        {
            _context = context;
        }

        public Recipe AddRecipe(Recipe newRecipe)
        {
            _context.Recipe.Add(newRecipe);
            _context.SaveChanges();
            return newRecipe;
        }

        public Recipe DeleteRecipe(int Id)
        {
            Recipe recipe = _context.Recipe.Find(Id);

            if (recipe != null)
            {
                _context.Recipe.Remove(recipe);
                _context.SaveChanges();
            }
            return recipe;
        }

        public Recipe EditRecipe(Recipe editedRecipe)
        {
            var recipe = _context.Recipe.Attach(editedRecipe);
            recipe.State = EntityState.Modified;
            _context.SaveChanges();
            return editedRecipe;
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            return _context.Recipe
                .Include(r => r.Ingredients)
                .Include(r => r.ApplicationUser)
                .Include(r => r.Comments)
                .Include(r => r.Hearts).ThenInclude(r => r.ApplicationUser)
                .ToList();
        }

        public Recipe GetRecipe(int Id)
        {
            return _context.Recipe
                .Include(r => r.Ingredients)
                .Include(r => r.ApplicationUser)
                .Include(r => r.Comments)
                .Include(r => r.Hearts)
                .AsNoTracking()
                .FirstOrDefault(r => r.Id == Id);
        }
    }
}
