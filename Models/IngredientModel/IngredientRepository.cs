using Microsoft.EntityFrameworkCore;
using SueRecipes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SueRecipes.Models.IngredientModel
{
    public class IngredientRepository : IIngredientRepository
    {

        private readonly AppDbContext _context;

        public IngredientRepository(AppDbContext context)
        {
            _context = context;
        }

        public Ingredient AddIngredient(Ingredient newIngredient)
        {
            _context.Ingredient.Add(newIngredient);
            _context.SaveChanges();
            return newIngredient;
        }

        public Ingredient DeleteIngredient(int Id)
        {
            Ingredient ingredient = _context.Ingredient.Find(Id);

            if (ingredient != null)
            {
                _context.Ingredient.Remove(ingredient);
                _context.SaveChanges();
            }
            return ingredient;
        }

        public Ingredient EditIngredient(Ingredient editedIngredient)
        {
            var ingredient = _context.Ingredient.Attach(editedIngredient);
            ingredient.State = EntityState.Modified;
            _context.SaveChanges();
            return editedIngredient;
        }

        public IEnumerable<Ingredient> GetAllIngredients()
        {
            return _context.Ingredient;
        }

        public Ingredient GetIngredient(int Id)
        {
            return _context.Ingredient.Find(Id);
        }
    }
}
