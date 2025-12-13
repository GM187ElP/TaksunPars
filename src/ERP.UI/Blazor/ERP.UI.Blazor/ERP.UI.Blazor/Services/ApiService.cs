namespace ERP.UI.Blazor.Services;

using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

    public class ApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly NavigationManager _navigationManager;
        private readonly ILogger<ApiService> _logger;

        public ApiService(
            IHttpClientFactory httpClientFactory,
            NavigationManager navigationManager,
            ILogger<ApiService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _navigationManager = navigationManager;
            _logger = logger;
        }

        private void HandleUnauthorized()
        {
            _logger.LogWarning("⚠️ API returned 401 Unauthorized - redirecting to logout");
            _navigationManager.NavigateTo("/logout", forceLoad: true);
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                HandleUnauthorized();
            }

            return response;
        }

        public async Task<T?> GetFromJsonAsync<T>(string url)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                HandleUnauthorized();
                return default;
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.PostAsync(url, content);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                HandleUnauthorized();
            }

            return response;
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, T data)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.PostAsJsonAsync(url, data);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                HandleUnauthorized();
            }

            return response;
        }

        public async Task<HttpResponseMessage> PutAsync(string url, HttpContent content)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.PutAsync(url, content);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                HandleUnauthorized();
            }

            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.DeleteAsync(url);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                HandleUnauthorized();
            }

            return response;
        }
}