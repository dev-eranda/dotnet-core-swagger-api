using System.Threading.Tasks;
using api.Models;

namespace api.interfaces
{
    public interface IFMPService
    {
        Task<Stock?> FindStockBySymbolAsync(string symbol);
    }
}
