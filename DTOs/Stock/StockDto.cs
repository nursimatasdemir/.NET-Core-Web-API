namespace api.DTOs.Stock;

public class StockDto
{
    public int Id { get; set; }

    public string Symbol { get; set; } = string.Empty;

    public string CompanyName { get; set; } = string.Empty;
    
    public string Industry { get; set; } = string.Empty;

    public long MarketCap { get; set; }
    //Comments were here but we don't want them in this scenario
    //These are the props we want to return as a new object
}