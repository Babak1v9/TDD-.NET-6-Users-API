using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD.NET6API.Controllers;
using TDD.NET6API.Models;

namespace TDD.NET6API.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() => new()
        {
            new User
            {
                    Id = 1,
                    Name = "Test user 1",
                    Address = new Address()
                    {
                        Street = "123 Main Street",
                        City = "LA",
                        ZipCode = "3212"
                    },
                    Email = "test@test.com"
            },
            new User
            {
                    Id = 2,
                    Name = "Test user 1",
                    Address = new Address()
                    {
                        Street = "123 Main Street",
                        City = "LA",
                        ZipCode = "3212"
                    },
                    Email = "test@test.com"
            },
            new User
            {
                    Id = 3,
                    Name = "Test user 3",
                    Address = new Address()
                    {
                        Street = "123 Main Street",
                        City = "LA",
                        ZipCode = "3212"
                    },
                    Email = "test@test.com"
            }
        };
    }
}
