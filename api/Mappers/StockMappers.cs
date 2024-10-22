using System;
using System.Linq;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockResponseDto ToStockDto(this Stock stockModel)
        {
            if (stockModel == null) throw new ArgumentNullException(nameof(stockModel));

            return new StockResponseDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStock(this StockCreateRequestDto stockCreateDto)
        {
            if (stockCreateDto == null) throw new ArgumentNullException(nameof(stockCreateDto));

            return new Stock
            {
                Symbol = stockCreateDto.Symbol,
                CompanyName = stockCreateDto.CompanyName,
                Purchase = stockCreateDto.Purchase,
                LastDiv = stockCreateDto.LastDiv,
                Industry = stockCreateDto.Industry,
                MarketCap = stockCreateDto.MarketCap,
            };
        }

        public static Stock ToFMPStock(this FMPStock fmpStock)
        {
            if (fmpStock == null) throw new ArgumentNullException(nameof(fmpStock));

            return new Stock
            {
                Symbol = fmpStock.symbol,
                CompanyName = fmpStock.companyName,
                Purchase = (decimal)fmpStock.price,
                LastDiv = (decimal)fmpStock.lastDiv,
                Industry = fmpStock.industry,
                MarketCap = fmpStock.mktCap,
            };
        }

    }
}
