using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.interfaces;
using api.Models;
using api.Dtos.Stock;
using api.Helpers;
using System.Linq;
using System;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllStocksAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(stock => stock.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.ComapnyName))
            {
                stocks = stocks.Where(stock => stock.CompanyName.Contains(query.ComapnyName));
            }

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(stock => stock.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.isDesending ? stocks.OrderByDescending(stock => stock.Symbol) : stocks.OrderBy(stock => stock.Symbol);
                }
            }

            return await stocks.ToListAsync();
        }

        public async Task<Stock?> GetStockByIdAsync(int id)
        {
            return await _context.Stocks.Include(stock => stock.Comments).FirstOrDefaultAsync(stock => stock.Id == id);
        }

        public async Task<Stock> CreateStockAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();

            return stock;
        }

        public async Task<Stock?> UpdateStockAsync(int id, StockUpdateRequestDto stockUpdateDto)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);
            if (stock == null)
            {
                return null;
            }

            stock.Symbol = stockUpdateDto.Symbol;
            stock.CompanyName = stockUpdateDto.CompanyName;
            stock.Purchase = stockUpdateDto.Purchase;
            stock.LastDiv = stockUpdateDto.LastDiv;
            stock.Industry = stockUpdateDto.Industry;
            stock.MarketCap = stockUpdateDto.MarketCap;
            await _context.SaveChangesAsync();

            return stock;
        }

        public async Task<Stock?> DeleteStockByIdAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);
            if (stock == null)
            {
                return null;
            }

            // delete is not asyn function
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return stock;
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(stock => stock.Id == id);
        }
    }
}
