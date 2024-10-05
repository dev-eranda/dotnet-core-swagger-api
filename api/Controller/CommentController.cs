using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.interfaces;
using api.Mappers;

namespace api.Controller
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRespository _commentRepo;

        public CommentController(ICommentRespository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentRepo.GetAllCommentAsync();
            var commentDto = comments.Select(c => c.ToCommentDto());

            return Ok(commentDto);
        }
    }
}
