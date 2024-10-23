using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using api.Extensions;
using api.interfaces;
using api.Models;

namespace api.Controller
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        private readonly IFMPService _fmpService;


        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepo, IFMPService fmpService)
        {
            _userManager = userManager;
            _stockRepo = stockRepository;
            _portfolioRepo = portfolioRepo;
            _fmpService = fmpService;
        }

        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfiolio = await _portfolioRepo.GetUserPortfolio(appUser!);
            return Ok(userPortfiolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePortfolio(string symbol)
        {
            var username = User.GetUserName();

            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null)
                return BadRequest("User not found");

            var stock = await _stockRepo.GetStockBySymbolAsync(symbol);
            if (stock == null)
            {
                var fmpStock = await _fmpService.FindStockBySymbolAsync(symbol);
                if (fmpStock == null)
                {
                    return BadRequest("Stock does not exist");
                }

                stock = await _stockRepo.CreateStockAsync(fmpStock);
                if (stock == null)
                {
                    return StatusCode(500, "Failed to create stock");
                }
            }

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser!);
            if (userPortfolio.Any(p => p.Symbol.ToLower() == symbol.ToLower()))
                return BadRequest("Cannot add same stock to portfolio");

            var portfolioModel = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id
            };

            await _portfolioRepo.CreatePortfolio(portfolioModel);
            return Created($"api/portfolios/", null);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null)
            {
                return BadRequest("User not found");
            }

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            // var filteredStock = userPortfolio.Where(stock => stock.Symbol.ToLower() == symbol.ToLower()).ToList();

            // Use FirstOrDefault to directly get the matching stock
            var filteredStock = userPortfolio.FirstOrDefault(stock => stock.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase));


            if (filteredStock == null)
            {
                return BadRequest("Stock not found in your portfolio");
            }

            var result = await _portfolioRepo.DeletePortfolio(appUser, symbol);
            if (!result)
            {
                return StatusCode(500, "Error deleting stock from portfolio");
            }

            return Ok("Stock deleted successfully");
        }
    }
}
