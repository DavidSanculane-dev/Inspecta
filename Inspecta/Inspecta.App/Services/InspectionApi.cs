using System.Net.Http.Json;
using Inspecta.Shared.Entities;

namespace Inspecta.App.Services
{
    public class InspectionApi
    {

        private readonly HttpClient _http;
        public InspectionApi(HttpClient http) => _http = http;

        public Task<List<Inspection>?> GetAllAsync()
            => _http.GetFromJsonAsync<List<Inspection>>("api/inspections");

        public async Task<Inspection?> CreateFromChecklistAsync(int checklistId)
        {
            var res = await _http.PostAsync($"api/inspections?checklistId={checklistId}", null);
            return res.IsSuccessStatusCode ? await res.Content.ReadFromJsonAsync<Inspection>() : null;
        }


    }
}
