using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.comment
{
    public class CommentCreateRequestDto
    {

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

    }
}
