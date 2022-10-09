using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TDD.NET6API.Config;
using TDD.NET6API.Models;
using TDD.NET6API.Services;
using TDD.NET6API.UnitTests.Fixtures;
using TDD.NET6API.UnitTests.Helpers;

namespace TDD.NET6API.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            // arrange
            var exptectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(exptectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UsersService(httpClient, config);

            // act
            await sut.GetAllUsers();

            // assert
            handlerMock
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>()
                );          
        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
        {
            // arrange
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
            var httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UsersService(httpClient, config);

            // act
            var result = await sut.GetAllUsers();

            // assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            // arrange
            var exptectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(exptectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UsersService(httpClient, config);

            // act
            var result = await sut.GetAllUsers();

            // assert
            result.Count.Should().Be(exptectedResponse.Count);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
        {
            // arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var endpoint = "https://example.com/users";
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse, endpoint);

            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UsersService(httpClient, config);

            // act
            var result = await sut.GetAllUsers();

            var uri = new Uri(endpoint);

            // assert
            handlerMock
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(
                        req => req.Method == HttpMethod.Get 
                        && req.RequestUri == uri),
                    ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}
