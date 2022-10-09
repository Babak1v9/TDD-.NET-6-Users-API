using Microsoft.Extensions.Options;
using TDD.NET6API.Config;
using TDD.NET6API.Models;

namespace TDD.NET6API.Services
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient _httpClient;
        private readonly UsersApiOptions _apiConfig;


        public UsersService()
        {

        }

        public UsersService(HttpClient httpClient, IOptions<UsersApiOptions> apiConfig)
        {
            _httpClient = httpClient;
            _apiConfig = apiConfig.Value;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var usersResponse = await _httpClient
                .GetAsync(_apiConfig.Endpoint);

            if (usersResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<User>();
            }
            var responseContent = usersResponse.Content;
            var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
            return allUsers.ToList();
        }
    }
}
