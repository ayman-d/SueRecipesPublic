using Microsoft.AspNetCore.Identity;
using SueRecipes.Models.CommentModel;
using SueRecipes.Models.HeartModel;
using SueRecipes.Models.RecipeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SueRecipes.Models.ApplicationUserModel
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Heart> Hearts { get; set; }
    }
}
