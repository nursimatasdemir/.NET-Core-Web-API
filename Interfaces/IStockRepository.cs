using api.DTOs.Stock;
using api.Models;

namespace api.Interfaces;

public abstract class IStockRepository
{
    public abstract Task<List<Stock>> GetAllAsync();
    public abstract Task<Stock?> GetByIdAsync(int id);//FirstOrDefault null değer döndürebilir Stock? bu yüzden
    public abstract Task<Stock> CreateAsync(Stock stockModel);
    public abstract Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
    public abstract Task<Stock?> DeleteAsync(int id);
}