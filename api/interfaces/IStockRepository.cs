using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync(QueryObject query);

        Task<Stock?> GetStockByIdAsync(int id);

        Task<Stock?> GetStockBySymbolAsync(string symbol);

        Task<Stock> CreateStockAsync(Stock stock);

        Task<Stock?> UpdateStockAsync(int id, StockUpdateRequestDto stockDto);

        Task<Stock?> DeleteStockByIdAsync(int id);

        Task<bool> StockExists(int id);
    }
}
