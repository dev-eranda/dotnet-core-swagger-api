using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.interfaces;
using api.Mappers;
using api.Dtos.comment;
using Microsoft.AspNetCore.Identity;
using api.Models;
using api.Extensions;
using Microsoft.AspNetCore.Authorization;
using api.Helpers;

namespace api.Controller
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRespository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFMPService _ifmpService;

        public CommentController(ICommentRespository commentRepo, IStockRepository stockRepo, UserManager<AppUser> userManager, IFMPService fmpService)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
            _ifmpService = fmpService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments([FromQuery] CommentQueryObject queryObject)
        {
            var comments = await _commentRepo.GetAllCommentAsync(queryObject);
            var commentDto = comments.Select(c => c.ToCommentDto());

            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{symbol:alpha}")]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromRoute] string symbol, [FromBody] CommentCreateRequestDto commentCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepo.GetStockBySymbolAsync(symbol);

            if (stock == null)
            {
                var fmpStock = await _ifmpService.FindStockBySymbolAsync(symbol);
                if (fmpStock == null)
                {
                    return BadRequest("Stock does not exist");
                }
                else
                {
                    stock = await _stockRepo.CreateStockAsync(fmpStock);
                }
            }

            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser == null)
                return BadRequest("User not found");

            var comment = commentCreateDto.ToComment(stock.Id);
            comment.AppUserId = appUser.Id; // bind UserId to CommentDto

            await _commentRepo.CreateCommentAsync(comment);

            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment.ToCommentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] CommentUpdateRequestDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.UpdateCommentAsync(id, commentDto);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCommentById(int id)
        {
            var comment = await _commentRepo.DeleteCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
