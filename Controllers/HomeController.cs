using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SueRecipes.Models.ApplicationUserModel;
using SueRecipes.Models.CommentModel;
using SueRecipes.Models.HeartModel;
using SueRecipes.Models.RecipeModel;
using SueRecipes.ViewModels;
using Microsoft.AspNetCore.Http;
using SueRecipes.Models.IngredientModel;
using System.IO;
using System.Collections.Generic;

namespace SueRecipes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IHeartRepository _heartRepository;

        public HomeController(
            ILogger<HomeController> logger,
            IRecipeRepository recipeRepository,
            ICommentRepository commentRepository,
            IHeartRepository heartRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIngredientRepository ingredientRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _recipeRepository = recipeRepository;
            _ingredientRepository = ingredientRepository;
            _commentRepository = commentRepository;
            _heartRepository = heartRepository;
        }

        /* Index Action (main page) */

        [HttpGet]
        public IActionResult Index(string searchTitle, string searchMealType, string searchCategory, string searchSpice, 
            string searchTime, string searchHot, string searchUser)
        {

            ViewData["searchTitle"] = searchTitle;

            if (!String.IsNullOrEmpty(searchTitle))
            {
                IndexViewModel indexViewModel = new IndexViewModel()
                {
                    Recipe = _recipeRepository.GetAllRecipes().Reverse().Where(r => r.Title.ToUpper().Contains(searchTitle.ToUpper()))
                };
                ViewData["browseBy"] = "\"" + searchTitle + "\"";
                return View(indexViewModel);
            }
            else if (!String.IsNullOrEmpty(searchMealType))
            {
                IndexViewModel indexViewModel = new IndexViewModel()
                {
                    Recipe = _recipeRepository.GetAllRecipes().Reverse().Where(r => r.MealType.ToString() == searchMealType)
                };
                ViewData["browseBy"] = searchMealType;
                return View(indexViewModel);
            }
            else if (!String.IsNullOrEmpty(searchCategory))
            {
                IndexViewModel indexViewModel = new IndexViewModel()
                {
                    Recipe = _recipeRepository.GetAllRecipes().Reverse().Where(r => r.Category.ToString() == searchCategory)
                };
                ViewData["browseBy"] = searchCategory;
                return View(indexViewModel);
            }
            else if (!String.IsNullOrEmpty(searchSpice))
            {
                IndexViewModel indexViewModel = new IndexViewModel()
                {
                    Recipe = _recipeRepository.GetAllRecipes().Reverse().Where(r => r.SpiceLevel.ToString() == searchSpice)
                };
                switch (searchSpice)
                {
                    case "0":
                        ViewData["browseBy"] = "Non Spicy";
                        break;
                    case "1":
                        ViewData["browseBy"] = "Mild Spicy";
                        break;
                    case "2":
                        ViewData["browseBy"] = "Spicy";
                        break;
                    case "3":
                        ViewData["browseBy"] = "Very Spicy";
                        break;
                }
                return View(indexViewModel);
            }

            else if (!String.IsNullOrEmpty(searchTime))
            {
                if (searchTime == "Fast")
                {
                    IndexViewModel indexViewModel = new IndexViewModel()
                    {
                        Recipe = _recipeRepository.GetAllRecipes().Reverse().Where(r => r.CookTime + r.PrepTime <= 15)
                    };
                    ViewData["browseBy"] = searchTime;
                    return View(indexViewModel);
                }
                else if (searchTime == "Normal")
                {
                    IndexViewModel indexViewModel = new IndexViewModel()
                    {
                        Recipe = _recipeRepository.GetAllRecipes().Reverse().Where(r => r.CookTime + r.PrepTime > 15 && r.CookTime + r.PrepTime <= 30)
                    };
                    ViewData["browseBy"] = searchTime;
                    return View(indexViewModel);
                }
                else
                {
                    IndexViewModel indexViewModel = new IndexViewModel()
                    {
                        Recipe = _recipeRepository.GetAllRecipes().Reverse().Where(r => r.CookTime + r.PrepTime > 30)
                    };
                    ViewData["browseBy"] = searchTime;
                    return View(indexViewModel);
                }
            }
            else if (!String.IsNullOrEmpty(searchHot))
            {
                IndexViewModel indexViewModel = new IndexViewModel()
                {
                    Recipe = _recipeRepository.GetAllRecipes().Reverse().Where(r => r.CreatedOn >= DateTime.Now.Date.AddDays(-7)).OrderByDescending(r => r.Hearts.Count())
                };
                ViewData["browseBy"] = "New & Popular";
                return View(indexViewModel);
            }
            else if (!String.IsNullOrEmpty(searchUser))
            {
                var recipe = _recipeRepository.GetAllRecipes().Where(r => r.ApplicationUserId == searchUser).FirstOrDefault();
                var userName = recipe.ApplicationUser.Name;

                IndexViewModel indexViewModel = new IndexViewModel()
                {
                    Recipe = _recipeRepository.GetAllRecipes().Reverse().Where(r => r.ApplicationUserId == searchUser)
                };
                ViewData["browseBy"] = "Author";
                ViewData["Author"] = userName;
                return View(indexViewModel);
            }
            else
            {
                IndexViewModel indexViewModel = new IndexViewModel()
                {
                    Recipe = _recipeRepository.GetAllRecipes().Reverse()
                };
                ViewData["browseBy"] = "None";
                return View(indexViewModel);
            }
        }

        /* User specific recipe list */

        [HttpGet]
        public IActionResult UserRecipes(string userId)
        {
            IndexViewModel indexViewModel = new IndexViewModel()
            {
                Recipe = _recipeRepository.GetAllRecipes().Reverse().Where(r => r.ApplicationUserId == userId)
            };

            return View(indexViewModel);
        }

        /* Detailed Page for a single recipe */

        [HttpGet]
        public IActionResult Details(int Id)
        {
            RecipeDetailViewModel recipeDetailViewModel = new RecipeDetailViewModel()
            {
                Recipe = _recipeRepository.GetRecipe(Id),
                Comments = _commentRepository.GetAllComments().Where(c => c.RecipeId == Id)
            };
            return View(recipeDetailViewModel);
        }

        /* Like Button */

        [HttpGet]
        public string GetLikeCount(int recipeId)
        {
            return _heartRepository.GetAllHearts().Where(h => h.RecipeId == recipeId).Count().ToString();
        }

        [HttpGet]
        public string CheckIfAlreadyLiked(int recipeId)
        {

            var userId = _userManager.GetUserId(HttpContext.User);

            Heart existingHeart = _heartRepository.GetHeart(userId, recipeId);

            return existingHeart != null ? "yes" : "no";
        }


        [Authorize]
        [HttpPost]
        public string Like(int recipeId)
        {

            var userId = _userManager.GetUserId(HttpContext.User);

            Heart existingHeart = _heartRepository.GetHeart(userId, recipeId);

            if (existingHeart == null)
            {
                Heart newHeart = new Heart
                {
                    ApplicationUserId = userId,
                    RecipeId = recipeId
                };

                _heartRepository.AddHeart(newHeart);
            }
            else
            {
                _heartRepository.DeleteHeart(userId, recipeId);
            }

            return _heartRepository.GetAllHearts().Where(h => h.RecipeId == recipeId).Count().ToString();
        }

        /* Create new recipe page */

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateRecipeViewModel model)
        {

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);

                string uniqueFileName = null;
                if (model.Image != null)
                {
                    string uploadFolder = Path.Combine("wwwroot", "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Image.CopyTo(fileStream);
                    }
                }

                Recipe newRecipe = new Recipe
                {
                    Title = model.Title,
                    Description = model.Description,
                    Instructions = model.Instructions,
                    MealType = model.MealType,
                    Servings = model.Servings,
                    PrepTime = model.PrepTime,
                    CookTime = model.CookTime,
                    Category = model.Category,
                    SpiceLevel = model.SpiceLevel,
                    ApplicationUserId = userId,
                    ImagePath = uniqueFileName
                };

                _recipeRepository.AddRecipe(newRecipe);

                foreach (var item in model.Ingredients)
                {
                    Ingredient ingredient = new Ingredient
                    {
                        RecipeId = newRecipe.Id,
                        Measure = item.Measure,
                        Unit = item.Unit,
                        Name = item.Name
                    };

                    _ingredientRepository.AddIngredient(ingredient);
                }

                return RedirectToAction("index", "home");
            }
            return View();
        }

        /* Test */
        public string Test(int recipeId)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            return _heartRepository.GetAllHearts().Where(h => h.RecipeId == recipeId).Count().ToString();
        }

        /* Delete Recipe */
        [Authorize]
        public IActionResult Delete(int recipeId)
        {

            var applicationUserId = _userManager.GetUserId(HttpContext.User);

            _recipeRepository.DeleteRecipe(recipeId);

            return RedirectToAction("userrecipes", "home", new { userId = applicationUserId });
        }

        /* Edit Recipe */
        [Authorize]
        [HttpGet]
        public IActionResult EditRecipe(int recipeId)
        {

            Recipe recipe = _recipeRepository.GetRecipe(recipeId);

            List<Ingredient> ingredients = _ingredientRepository.GetAllIngredients().Where(r => r.RecipeId == recipeId).ToList();

            EditRecipeViewModel editRecipeViewModel = new EditRecipeViewModel()
            {
                Id = recipeId,
                Title = recipe.Title,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                ExistingImagePath = recipe.ImagePath,
                MealType = recipe.MealType,
                Servings = recipe.Servings,
                PrepTime = recipe.PrepTime,
                CookTime = recipe.CookTime,
                Category = recipe.Category,
                SpiceLevel = recipe.SpiceLevel,
                Ingredients = ingredients
            };

            return View(editRecipeViewModel);

        }

        [Authorize]
        [HttpPost]
        public IActionResult EditRecipe(EditRecipeViewModel model)
        {
            if (ModelState.IsValid)
            {

                List<Ingredient> existingIngredients = _ingredientRepository.GetAllIngredients().Where(i => i.RecipeId == model.Id).ToList();
                foreach (Ingredient ingredient in existingIngredients)
                {
                    _ingredientRepository.DeleteIngredient(ingredient.Id);
                }

                Recipe recipe = _recipeRepository.GetRecipe(model.Id);

                recipe.Id = model.Id;
                recipe.Title = model.Title;
                recipe.Description = model.Description;
                recipe.Instructions = model.Instructions;
                recipe.MealType = model.MealType;
                recipe.Servings = model.Servings;
                recipe.PrepTime = model.PrepTime;
                recipe.CookTime = model.CookTime;
                recipe.Category = model.Category;
                recipe.SpiceLevel = model.SpiceLevel;
                recipe.Ingredients = model.Ingredients;

                /*foreach (var newIngredient in model.Ingredients)
                {
                    Ingredient ingredient = new Ingredient()
                    {
                        RecipeId = recipe.Id,
                        Measure = newIngredient.Measure,
                        Unit = newIngredient.Unit,
                        Name = newIngredient.Name
                    };

                    _ingredientRepository.AddIngredient(ingredient);

                }*/

                if (model.Image != null)
                {
                    if (model.ExistingImagePath != null)
                    {
                        string deleteFilePath = Path.Combine("wwwroot", "images", model.ExistingImagePath);
                        System.IO.File.Delete(deleteFilePath);
                    }

                    string uploadFolder = Path.Combine("wwwroot", "images");
                    recipe.ImagePath = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadFolder, recipe.ImagePath);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Image.CopyTo(fileStream);
                    }

                }

                _recipeRepository.EditRecipe(recipe);

                return RedirectToAction("userrecipes", "home", new { userId = recipe.ApplicationUserId });
            }

            return View();
        }
    }
}