using System.Collections.Generic;

namespace SueRecipes.Models.HeartModel
{
    public interface IHeartRepository
    {
        IEnumerable<Heart> GetAllHearts();
        Heart GetHeart(string ApplicationUserId, int RecipeId);
        Heart AddHeart(Heart newHeart);
        Heart DeleteHeart(string ApplicationUserId, int RecipeId);
    }
}
