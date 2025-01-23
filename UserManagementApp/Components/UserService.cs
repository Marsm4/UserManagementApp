using Amazon.IdentityManagement.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagementApp.Models;

namespace UserManagementApp.Components
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> RegisterUserAsync(RegisterUserDto userDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/users/register", userDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/users/login", loginDto);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<List<Models.User>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Models.User>>("api/users/all");
        }

        public async Task<string> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/users/{id}");
            return await response.Content.ReadAsStringAsync();
        }
    }

}

