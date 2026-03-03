using System.Net.Http.Json;
using Inspecta.Shared.Entities;

namespace Inspecta.App.Services
{
    public class ChecklistApi
    {

        private readonly HttpClient _http;
        public ChecklistApi(HttpClient http) => _http = http;

        public Task<List<Checklist>?> GetAllAsync()
            => _http.GetFromJsonAsync<List<Checklist>>("api/checklists");

        public async Task<Checklist?> CreateAsync(Checklist dto)
        {
            var res = await _http.PostAsJsonAsync("api/checklists", dto);
            return res.IsSuccessStatusCode ? await res.Content.ReadFromJsonAsync<Checklist>() : null;
        }


    }
}
