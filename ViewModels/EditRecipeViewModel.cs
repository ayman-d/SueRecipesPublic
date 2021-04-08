using Microsoft.AspNetCore.Http;
using SueRecipes.Models.IngredientModel;
using SueRecipes.Models.RecipeModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SueRecipes.ViewModels
{
    public class EditRecipeViewModel : CreateRecipeViewModel
    {

        public int Id { get; set; }
        public string ExistingImagePath { get; set; }
        /*
                [Required]
                [MaxLength(40, ErrorMessage = "Sorry, your title is too long (max 40 characters)")]
                public string Title { get; set; }

                [Required]
                [MaxLength(200, ErrorMessage = "Sorry, your description is too long (max 200 characters")]
                public string Description { get; set; }

                [Required]
                public string Instructions { get; set; }

                public string ImagePath { get; set; }

                public IFormFile Image { get; set; }

                [Required]
                public MealType MealType { get; set; }

                [Required]
                [Range(1, 8, ErrorMessage = "Please choose a number between 1 and 8")]
                public int Servings { get; set; }

                [Required]
                [Range(0, 240, ErrorMessage = "Allowed values: 0 - 240 minutes")]
                [Display(Name = "Prep Time")]
                public int PrepTime { get; set; }

                [Required]
                [Range(0, 240, ErrorMessage = "Allowed values: 0 - 240 minutes")]
                [Display(Name = "Cook Time")]
                public int CookTime { get; set; }

                [Required]
                public Category Category { get; set; }

                [Required]
                [Display(Name = "Spice Level")]
                [Range(0, 3, ErrorMessage = "Choose a number between 0 and 3")]
                public int SpiceLevel { get; set; }

                public List<Ingredient> Ingredients { get; set; }*/
    }
}
