using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable
namespace api.Models;

[Table("Portfolios")]
public class Portfolio
{
    [MaxLength(255)] public string AppUserId { get; set; }
    [ForeignKey("AppUserId")] public AppUser AppUser { get; set; }
    [MaxLength(255)] public int StockId { get; set; }
    [ForeignKey("StockId")] public Stock Stock { get; set; }
}