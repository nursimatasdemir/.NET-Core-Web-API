using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/portfolio")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IStockRepository _stockRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    
    public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
    {
        _userManager = userManager;
        _stockRepository = stockRepository;
        _portfolioRepository = portfolioRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
        return Ok(userPortfolio);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePortfolio(string symbol)
    {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        
        var stock = await _stockRepository.GetBySymbolAsync(symbol);
        
        if(stock == null)
            return BadRequest("Stock not found");
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

        if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower()))
            return BadRequest("Symbol already exists");

        var portfolioModel = new Portfolio
        {
            StockId = stock.StockId,
            AppUserId = appUser.Id,
        };
        
        await _portfolioRepository.CreateAsync(portfolioModel);

        if (portfolioModel == null)
        {
            return StatusCode(500, "Could not create portfolio");
        }
        else
        {
            return Ok("Portfolio created");
        }
    }

    [HttpDelete]
    [Authorize]

    public async Task<IActionResult> DeletePortfolio(string symbol)
    {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        
        if(appUser == null)
            return BadRequest("User not found");
        
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
        
        var filteredStock = userPortfolio.Where(e => e.Symbol.ToLower()== symbol.ToLower());

        if (filteredStock.Count() == 1)
        {
            await _portfolioRepository.DeleteAsync(appUser, symbol);
            return Ok("Portfolio deleted");
        }
        else
        {
            return BadRequest("Stock is not on your portfolio");
        }

        return Ok();
    }
}