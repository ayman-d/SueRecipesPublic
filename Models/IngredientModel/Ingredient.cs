using System;
using SueRecipes.Models.RecipeModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SueRecipes.Models.IngredientModel
{
    public class Ingredient
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public double Measure { get; set; }
        public UnitOfMeasure Unit { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public Recipe Recipe { get; set; }

        public Ingredient()
        {
            this.CreatedOn = DateTime.Now;
        }

    }
}
