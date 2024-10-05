using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.interfaces;
using api.Models;

namespace api.Repository
{
    public class CommentRepository : ICommentRespository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllCommentAsync()
        {
            return await _context.Comments.ToListAsync();
        }
    }
}
