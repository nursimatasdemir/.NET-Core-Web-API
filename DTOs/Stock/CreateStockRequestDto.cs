using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Stock;

public class CreateStockRequestDto
{
    [Required]
    [MaxLength(10, ErrorMessage = "Stock symbol cannot be longer than 10 characters.")] 
    public string Symbol { get; set; } = string.Empty; 
    [Required]
    [MaxLength(100, ErrorMessage = "Stock company name cannot be longer than 100 characters.")]
    public string CompanyName { get; set; } = string.Empty;
    [Required]
    [Range(1, 1000000000, ErrorMessage = "Stock quantity should be between 1 and 10000000.")]
    public decimal Purchase { get; set; } 
    [Required]
    [Range(0.001, 100, ErrorMessage = "Stock quantity should be between 0.001 and 100.")]
    public decimal LastDiv { get; set; } 
    [Required]
    [MaxLength(100, ErrorMessage = "Industry cannot be longer than 100 characters.")]
    public string Industry { get; set; } = string.Empty;

    [Range(1, 50000000000, ErrorMessage = "Stock quantity should be between 1 and 50000000.")]
    public long MarketCap { get; set; } 

}