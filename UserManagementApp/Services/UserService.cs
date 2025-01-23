﻿using System.Net.Http.Json;
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

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("api/users/all");
        }

        public async Task<string> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/users/{id}");
            return await response.Content.ReadAsStringAsync();
        }
    }
}

