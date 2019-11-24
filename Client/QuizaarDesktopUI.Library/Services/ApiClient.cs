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
            using (HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/Categories?shallow=true"))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                return await response.Content.ReadAsAsync<List<CategoryShallowDTO>>();
            }
        }

        public async Task<List<CategoryDTO>> GetFullCategories()
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/Categories?shallow=false"))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                return await response.Content.ReadAsAsync<List<CategoryDTO>>();
            }
        }

        public async Task<IEnumerable<ICategoryDTO>> GetCategories(bool shallow)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync($"/api/v1/Categories?shallow={ shallow }"))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                if (shallow)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<CategoryShallowDTO>>();
                }
                else
                {
                    return await response.Content.ReadAsAsync<IEnumerable<CategoryDTO>>();
                }
            }
        }

        public async Task PostCategory(CategoryShallowDTO category)
        {
            using (HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/v1/Categories", category))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        //public async Task PostDummyQuestions()
        //{
        //    List<CreateQuestionForm> dummyQuestions = new List<CreateQuestionForm>();

        //    dummyQuestions.Add(new CreateQuestionForm
        //    {
        //        CategoryId = 4,
        //        Content = "Why?"
        //    });
        //    dummyQuestions.Add(new CreateQuestionForm
        //    {
        //        CategoryId = 4,
        //        Content = "Why not?"
        //    });

        //    foreach (CreateQuestionForm question in dummyQuestions)
        //    {
        //        using (HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/v1/Questions", question))
        //        {
        //            if (response.IsSuccessStatusCode == false)
        //            {
        //                throw new Exception(response.ReasonPhrase);
        //            }
        //        }
        //    }
        //}
    }
}
