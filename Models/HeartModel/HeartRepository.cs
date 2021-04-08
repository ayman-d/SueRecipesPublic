using Microsoft.EntityFrameworkCore;
using SueRecipes.Data;
using System.Collections.Generic;
using System.Linq;

namespace SueRecipes.Models.HeartModel
{
    public class HeartRepository : IHeartRepository
    {

        private readonly AppDbContext _context;

        public HeartRepository(AppDbContext context)
        {
            _context = context;
        }

        public Heart AddHeart(Heart newHeart)
        {
            _context.Heart.Add(newHeart);
            _context.SaveChanges();
            return newHeart;
        }

        public Heart DeleteHeart(string ApplicationUserId, int RecipeId)
        {
            Heart heart = _context.Heart.Find(ApplicationUserId, RecipeId);
            if (heart != null)
            {
                _context.Remove(heart);
                _context.SaveChanges();
            }
            return heart;
        }

        public IEnumerable<Heart> GetAllHearts()
        {
            return _context.Heart
                .Include(h => h.ApplicationUser)
                .Include(h => h.Recipe)
                .ToList();
        }

        public Heart GetHeart(string ApplicationUserId, int RecipeId)
        {
            return _context.Heart
                .Include(h => h.ApplicationUser)
                .Include(h => h.Recipe)
                .AsNoTracking()
                .FirstOrDefault(h => h.ApplicationUserId == ApplicationUserId && h.RecipeId == RecipeId);
        }
    }
}
