using System;
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

        // public static void UpdateFromDto(this Stock stockModel, StockUpdateRequestDto stockUpdateDto)
        // {
        //     // if (stockModel == null) throw new ArgumentNullException(nameof(stockModel));
        //     // if (stockUpdateDto == null) throw new ArgumentNullException(nameof(stockUpdateDto));

        //     stockModel.Symbol = stockUpdateDto.Symbol;
        //     stockModel.CompanyName = stockUpdateDto.CompanyName;
        //     stockModel.Purchase = stockUpdateDto.Purchase;
        //     stockModel.LastDiv = stockUpdateDto.LastDiv;
        //     stockModel.Industry = stockUpdateDto.Industry;
        //     stockModel.MarketCap = stockUpdateDto.MarketCap;
        // }
    }
}
