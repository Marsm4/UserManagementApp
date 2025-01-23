using System.Net.Http.Json;
using UserManagementApp.Models;

namespace UserManagementApp.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Регистрация пользователя
        public async Task<string> RegisterUserAsync(RegisterUserDto userDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/users/register", userDto);
            return await response.Content.ReadAsStringAsync();
        }

        // Авторизация пользователя
        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/users/login", loginDto);
            return await response.Content.ReadAsStringAsync();
        }

        // Получение всех пользователей
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("api/users/all");
        }

        // Получение пользователя по ID
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<User>($"api/users/{id}");
        }

        // Удаление пользователя
        public async Task<string> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/users/{id}");
            return await response.Content.ReadAsStringAsync();
        }

    }
}