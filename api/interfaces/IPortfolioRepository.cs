using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);

        Task<Portfolio?> CreatePortfolio(Portfolio portfolio);

        Task<bool> DeletePortfolio(AppUser user, string symbol);
    }
}
