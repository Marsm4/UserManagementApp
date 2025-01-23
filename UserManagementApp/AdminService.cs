using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using MyBlazorApp.Models;
using Microsoft.AspNetCore.Components;

namespace MyBlazorApp.Data
{
    public class AdminService
    {
        private readonly HttpClient _httpClient;

        public AdminService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> LoginAsync(AdminLogin adminLogin)
        {
            var loginContent = new StringContent(JsonSerializer.Serialize(adminLogin), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("users/login", loginContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync("users/all");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<IEnumerable<User>>();
                return data;
            }
            return null;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"users/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }
            return null;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("users/register", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUserAsync(int id, User user)
        {
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"users/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"users/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
