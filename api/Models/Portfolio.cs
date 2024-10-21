using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public required string AppUserId { get; set; }
        public required int StockId { get; set; }

        public required AppUser AppUser { get; set; }
        public required Stock Stock { get; set; }
    }
}
