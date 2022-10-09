using TDD.NET6API.Models;

namespace TDD.NET6API.Services
{
    public interface IUsersService
    {
        public Task<List<User>> GetAllUsers();
    }
}
