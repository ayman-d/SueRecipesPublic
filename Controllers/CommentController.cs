using System;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SueRecipes.Models.ApplicationUserModel;
using SueRecipes.Models.CommentModel;
using SueRecipes.Models.RecipeModel;

namespace SueRecipes.Controllers
{
    public class CommentController : Controller
    {

        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRecipeRepository _recipeRepository;

        public CommentController(ICommentRepository commentRepository, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IRecipeRepository recipeRepository)
        {
            _commentRepository = commentRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _recipeRepository = recipeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public JsonResult Create(string commentBody, int recipeId, string applicationUserId)
        {
            string userName = _signInManager.UserManager.GetUserAsync(User).Result.Name;
            Comment newComment = new Comment
            {
                Body = commentBody,
                RecipeId = recipeId,
                ApplicationUserId = applicationUserId,
                ApplicationUserName = userName
            };

            _commentRepository.AddComment(newComment);

            string jsonString = JsonConvert.SerializeObject(  new 
            { userId = newComment.ApplicationUserId, 
            userName = newComment.ApplicationUserName,
            commentBody = newComment.Body,
            commentCreatedOn = newComment.CreatedOn,
            commentId = newComment.Id.ToString()}, 
            Formatting.Indented,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return Json(jsonString);
        }

        [HttpGet]
        public IActionResult GetAll(string recipeId)
        {
            var comments = _commentRepository.GetAllCommentsSafe().Where(c => c.RecipeId == int.Parse(recipeId));
            string jsonString = JsonConvert.SerializeObject(comments, Formatting.Indented, 
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore } );
            return Json(jsonString);
        }

        [Authorize]
        public void Delete(string commentId)
        {
            _commentRepository.DeleteComment(int.Parse(commentId));
        }

    }
}
