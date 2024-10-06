using System.Collections.Generic;
using api.Dtos.comment;

namespace api.Dtos.Stock
{
    public class StockResponseDto
    {
        public int Id { get; set; }

        public string Symbol { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public decimal Purchase { get; set; }

        public decimal LastDiv { get; set; }

        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }

        public  List<CommentResponseDto>? Comments { get; set; }
    }
}
