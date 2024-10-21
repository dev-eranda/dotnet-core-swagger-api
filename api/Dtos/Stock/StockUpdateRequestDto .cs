using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock
{
    public class StockUpdateRequestDto
    {
        [MaxLength(10, ErrorMessage = "The symbol is too long. It cannot exceed 10 characters.")]
        public string Symbol { get; set; } = string.Empty;

        [MaxLength(10, ErrorMessage = "The company name is too long. It cannot exceed 10 characters.")]
        public string CompanyName { get; set; } = string.Empty;

        [Range(1, 1000000000)]
        public decimal Purchase { get; set; }

        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }

        [MaxLength(10, ErrorMessage = "The industry is too long. It cannot exceed 10 characters.")]
        public string Industry { get; set; } = string.Empty;

        [Range(1, 5000000000)]
        public long MarketCap { get; set; }
    }
}
