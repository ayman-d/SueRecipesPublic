using SueRecipes.Models.ApplicationUserModel;
using SueRecipes.Models.CommentModel;
using SueRecipes.Models.IngredientModel;
using SueRecipes.Models.HeartModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SueRecipes.Models.RecipeModel
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [MaxLength(200, ErrorMessage = "Sorry, your description is too long")]
        public string  Description { get; set; }
        public string Instructions { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ImagePath { get; set; }
        public MealType MealType { get; set; }
        [Range(1,8, ErrorMessage = "Please choose a number between 1 and 8")]
        public int Servings { get; set; }
        [Display(Name = "Prep Time")]
        public int PrepTime { get; set; }
        [Display (Name = "Cook Time")]
        public int CookTime { get; set; }
        public Category Category { get; set; }
        [Display(Name = "Spice Level")]
        [Range(0,3, ErrorMessage = "Choose a number between 0 and 3")]
        public int SpiceLevel { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Heart> Hearts { get; set; }

        public Recipe()
        {
            this.CreatedOn = DateTime.Now;
        }
    }
}

