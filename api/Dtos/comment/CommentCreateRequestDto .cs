using System.ComponentModel.DataAnnotations;

namespace api.Dtos.comment
{
    public class CommentCreateRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "The title is too short. It must be at least 5 characters long.")]
        [MaxLength(280, ErrorMessage = "The title is too long. It cannot exceed 280 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "The content is too short. It must be at least 5 characters long.")]
        [MaxLength(280, ErrorMessage = "The content is too long. It cannot exceed 280 characters.")]
        public string Content { get; set; } = string.Empty;
    }
}
