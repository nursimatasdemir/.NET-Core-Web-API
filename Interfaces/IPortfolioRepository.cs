using api.Models;

namespace api.Interfaces;

public abstract class IPortfolioRepository
{
   public abstract Task<List<Stock>> GetUserPortfolio(AppUser user);
   public abstract Task<Portfolio> CreateAsync (Portfolio portfolio);
   

}