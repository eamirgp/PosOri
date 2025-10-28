using Pos.App.Features.UnitOfMeasure.Models;
using Pos.App.Services.Interfaces;
using System.Net.Http.Json;

namespace Pos.App.Services.Implementations
{
    public class UnitOfMeasureService : IUnitOfMeasureService
    {
        private readonly HttpClient _httpClient;

        public UnitOfMeasureService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UnitOfMeasureSelectModel>> GetAllUnitOfMeasuresSelectAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<UnitOfMeasureSelectModel>>("api/UnitOfMeasure");
            return response ?? new List<UnitOfMeasureSelectModel>();
        }
    }
}
