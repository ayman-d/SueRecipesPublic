using SueRecipes.Models.CommentModel;
using SueRecipes.Models.RecipeModel;
using System.Collections.Generic;

namespace SueRecipes.ViewModels
{
    public class RecipeDetailViewModel
    {
        public Recipe Recipe { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
