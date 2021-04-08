using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SueRecipes.Models.CommentModel
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetAllComments();
        Comment GetComment(int Id);
        Comment AddComment(Comment newComment);
        Comment EditComment(Comment editedComment);
        Comment DeleteComment(int Id);
        IEnumerable<Comment> GetAllCommentsSafe();
    }
}
