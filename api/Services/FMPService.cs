using System;
using System.Net.Http;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.interfaces;
using api.Mappers;
using api.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace api.Services
{
    public class FMPService : IFMPService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;


        public FMPService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<Stock> FindStockBySymbolAsync(string symbol)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apiKey={_config["FMPKey"]}");

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(content))
                {
                    var stockData = JsonConvert.DeserializeObject<FMPStock[]>(content);

                    if (stockData != null && stockData.Length > 0)
                    {
                        return stockData[0].ToFMPStock();
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}