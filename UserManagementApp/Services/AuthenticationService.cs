using Blazored.LocalStorage;

namespace UserManagementApp.Services
{
    public class AuthenticationService
    {
        private readonly ILocalStorageService _localStorageService;

        public AuthenticationService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        // Сохранение токена или роли пользователя
        public async Task SaveUserData(string token, string role)
        {
            await _localStorageService.SetItemAsync("authToken", token);
            await _localStorageService.SetItemAsync("userRole", role);
        }

        // Получение роли пользователя
        public async Task<string> GetUserRole()
        {
            return await _localStorageService.GetItemAsync<string>("userRole");
        }

        // Проверка, авторизован ли пользователь
        public async Task<bool> IsAuthenticated()
        {
            var token = await _localStorageService.GetItemAsync<string>("authToken");
            return !string.IsNullOrEmpty(token);
        }

        // Удаление данных пользователя (для выхода)
        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("authToken");
            await _localStorageService.RemoveItemAsync("userRole");
        }
    }
}
