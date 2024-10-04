using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.interfaces;
using api.Models;
using api.Dtos.Stock;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllStocksAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetStockByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task<Stock> CreateStockAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> UpdateStockAsync(int id, StockUpdateRequestDto stockUpdateDto)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

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
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null)
            {
                return null;
            }

            // delete is not asyn function
            _context.Stocks.Remove(stock);

            await _context.SaveChangesAsync();

            return stock;
        }
    }
}