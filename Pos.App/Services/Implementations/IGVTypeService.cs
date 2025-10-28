using Pos.App.Features.IGVType.Models;
using Pos.App.Services.Interfaces;
using System.Net.Http.Json;

namespace Pos.App.Services.Implementations
{
    public class IGVTypeService : IIGVTypeService
    {
        private readonly HttpClient _httpClient;

        public IGVTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<IGVTypeSelectModel>> GetAllIGVTypesSelectAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<IGVTypeSelectModel>>("api/IGVType");
            return response ?? new List<IGVTypeSelectModel>();
        }
    }
}
