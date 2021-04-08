using SueRecipes.Models.ApplicationUserModel;
using SueRecipes.Models.RecipeModel;
using System;

namespace SueRecipes.Models.HeartModel
{
    public class Heart
    {
        public string ApplicationUserId { get; set; }
        public int RecipeId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Recipe Recipe { get; set; }
        public DateTime CreatedOn { get; set; }

        public Heart()
        {
            this.CreatedOn = DateTime.Now;
        }
    }
}


