using Microsoft.AspNetCore.Mvc;
using SueRecipes.Models.RecipeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SueRecipes.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Recipe> Recipe { get; set; }
    }
}
