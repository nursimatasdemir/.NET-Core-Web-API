using api.Data;
using api.DTOs.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;
public class StockRepository : IStockRepository
{
    private readonly ApplicationDbContext _context;
    public StockRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public  async Task<List<Stock>> GetAllAsync()
    {
       return await _context.Stock.Include(c => c.Comments).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _context.Stock.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async  Task<Stock> CreateAsync(Stock stockModel)
    {
        await _context.Stock.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;
    }

    public async  Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
    {
        var existingstock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
        if (existingstock == null)
        {
            return null;
        }
        existingstock.Symbol = stockDto.Symbol;
        existingstock.CompanyName = stockDto.CompanyName;
        existingstock.Purchase = stockDto.Purchase;
        existingstock.LastDiv = stockDto.LastDiv;
        existingstock.Industry = stockDto.Industry;
        existingstock.MarketCap = stockDto.MarketCap;
        
        await _context.SaveChangesAsync();
        return existingstock;
    }

    public async  Task<Stock?> DeleteAsync(int id)
    {
        var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
        if (stockModel == null)
        {
            return null;
        }
        _context.Stock.Remove(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;
    }

    public async  Task<bool> StockExists(int id)
    {
        return await _context.Stock.AnyAsync(i => i.Id == id);
    }
}