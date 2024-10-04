using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.interfaces;
using api.Models;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Stock>> GetAllStocksAsync()
        {
            return _context.Stocks.ToListAsync();
        }
    }
}