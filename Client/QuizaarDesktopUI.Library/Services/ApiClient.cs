using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using QuizaarDesktopUI.Library.Models;

namespace QuizaarDesktopUI.Library.Services
{
    public class ApiClient : IApiClient
    {
        private HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            InitializeClient();
        }

        private void InitializeClient()
        {
            // TODO: Move this somewhere else
            string api = "https://localhost:44379/";

            _httpClient = new HttpClient { BaseAddress = new Uri(api) };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<CategoryShallowDTO>> GetShallowCategories()
        {
            var response = await _httpClient.GetAsync("/api/v1/Categories?shallow=true");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<CategoryShallowDTO>>();
        }

        public async Task<bool> PostCategory(CategoryShallowDTO category)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/v1/Categories", category);

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return false;
            }

            response.EnsureSuccessStatusCode();

            return true;
        }
    }
}
