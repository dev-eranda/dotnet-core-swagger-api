using System;
using api.Dtos.comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentResponseDto ToCommentDto (this Comment commentModel)
        {
            if(commentModel == null ) throw new ArgumentNullException(nameof(commentModel));

            return new CommentResponseDto {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId,
            };
        }

         public static Comment ToComment (this CommentCreateRequestDto commentCreateDto, int stockId)
        {
            if(commentCreateDto == null ) throw new ArgumentNullException(nameof(commentCreateDto));

            return new Comment {
                Title = commentCreateDto.Title,
                Content = commentCreateDto.Content,
                StockId = stockId
            };
        }
    }
}
