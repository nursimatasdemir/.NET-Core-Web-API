using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

[Table("Stock")]
public class Stock
{
    public int StockId { get; set; }

    [MaxLength(20)] public string Symbol { get; set; } = string.Empty; //We defined not null

    [MaxLength(255)] public string CompanyName { get; set; } = string.Empty; // not null property

    [Column(TypeName = "decimal(18,2)")]
    public decimal Purchase { get; set; } // in the next column we're going to specify money and we have to parse it
    //18 digits and 2 decimal places is going to specify money
    //we can't make it int because money should be storr like an exact amount to not make any mistakes

    [Column(TypeName = "decimal(18,2)")] public decimal LastDiv { get; set; } // div is for dividend ==> kar payı

    [MaxLength(255)] public string Industry { get; set; } = string.Empty;

    public long
        MarketCap
    {
        get;
        set;
    } // MarketCap ==> Piyasa değeri the marketcap can be very high prices so we need to store it as long

    public List<Comment> Comments { get; set; } =
        new List<Comment>(); // we created a Comment section to truly understand what List does

    public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
}