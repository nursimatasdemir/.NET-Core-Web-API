using api.DTOs.Stock;
using api.Models;

namespace api.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync();
    Task<Stock?> GetByIdAsync(int id);//FirstOrDefault null değer döndürebilir Stock? bu yüzden
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
    Task<Stock?> DeleteAsync(int id);
    Task<bool> StockExists(int id);
    
}