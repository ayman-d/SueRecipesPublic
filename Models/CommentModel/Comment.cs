using SueRecipes.Models.ApplicationUserModel;
using SueRecipes.Models.RecipeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SueRecipes.Models.CommentModel
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public string ApplicationUserId { get; set; }
        public string ApplicationUserName { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime CreatedOn { get; set; }

        public Comment()
        {
            this.CreatedOn = DateTime.Now;
        }
    }
}
