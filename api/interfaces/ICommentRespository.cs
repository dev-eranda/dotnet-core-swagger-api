using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.comment;
using api.Models;

namespace api.interfaces
{
    public interface ICommentRespository
    {
        Task<List<Comment>> GetAllCommentAsync();

        Task<Comment?> GetCommentByIdAsync(int id);

        Task<Comment> CreateCommentAsync(Comment comment);

        Task<Comment?> UpdateCommentAsync(int id, CommentUpdateRequestDto commentDto);
    }
}
