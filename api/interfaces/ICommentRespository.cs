using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.interfaces
{
    public interface ICommentRespository
    {
        Task<List<Comment>> GetAllCommentAsync();
    }
}
