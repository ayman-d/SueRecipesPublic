using Microsoft.EntityFrameworkCore;
using SueRecipes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SueRecipes.Models.CommentModel
{
    public class CommentRepository : ICommentRepository
    {

        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return _context.Comment
                .Include(c => c.ApplicationUser)
                .Include(c => c.Recipe)
                .ToList();
        }

        public Comment GetComment(int Id)
        {
            return _context.Comment
                .Include(c => c.ApplicationUser)
                .Include(c => c.Recipe)
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == Id);
        }

        public Comment AddComment(Comment newComment)
        {
            _context.Comment.Add(newComment);
            _context.SaveChanges();
            return newComment;
        }

        public Comment EditComment(Comment editedComment)
        {
            var comment = _context.Comment.Attach(editedComment);
            comment.State = EntityState.Modified;
            _context.SaveChanges();
            return editedComment;
        }

        public Comment DeleteComment(int Id)
        {
            Comment comment = _context.Comment.Find(Id);
            if (comment != null)
            {
                _context.Remove(comment);
                _context.SaveChanges();
            }
            return comment;
        }

        public IEnumerable<Comment> GetAllCommentsSafe()
        {
            return _context.Comment;
        }
    }
}
