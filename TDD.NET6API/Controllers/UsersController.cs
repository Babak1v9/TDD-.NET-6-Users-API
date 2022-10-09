using Microsoft.AspNetCore.Mvc;
using TDD.NET6API.Services;

namespace TDD.NET6API.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase 
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> Get() {
            var users = await _usersService.GetAllUsers();

            if (users.Any())
            {
                return Ok(users);
            }
            return NotFound();
        }
    }
}