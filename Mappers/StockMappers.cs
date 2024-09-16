using api.DTOs.Stock;
using api.Models;

namespace api.Mappers;

public static class StockMappers
{
    public static StockDto ToStockDto(this Stock stockModel)
    {
        //We will return this object as a new object
        return new StockDto()
        {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            Industry = stockModel.Industry,
            MarketCap = stockModel.MarketCap,
        };
    }
}