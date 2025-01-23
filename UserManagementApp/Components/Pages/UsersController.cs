using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagementApp.Models;
using UserManagementApp.Services;  // Подключение пространства имен для UserService

[ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // Регистрация пользователя
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userDto)
        {
            var result = await _userService.RegisterUserAsync(userDto);
            return Ok(result);  // Возвращает 200 OK с результатом
        }

        // Авторизация пользователя
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _userService.LoginAsync(loginDto);
            return Ok(result);  // Возвращает 200 OK с результатом
        }

        // Получение списка всех пользователей (для администратора)
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);  // Возвращает 200 OK с данными пользователей
        }

        // Удаление пользователя по ID (только администратор)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return Ok(result);  // Возвращает 200 OK с результатом удаления
        }
    }
