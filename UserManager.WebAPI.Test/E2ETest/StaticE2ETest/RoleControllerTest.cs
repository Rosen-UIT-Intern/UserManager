using System;
using System.Net;
using System.Reflection;
using System.Net.Http;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Xunit;
using Xunit.Abstractions;

using Newtonsoft.Json;

using UserManager.WebApi;
using UserManager.Contract.DTOs;
using UserManager.Dal;

using UserManager.AppService.Test.E2ETest;

namespace UserManager.AppService.Test.StaticE2ETest
{
    [Collection("StaticE2E")]
    public class RoleControllerTest
    {
        private readonly E2EControllerTestFixture fixture;
        private readonly ITestOutputHelper output;

        public RoleControllerTest(ITestOutputHelper output, E2EControllerTestFixture fixture)
        {
            this.output = output;
            this.fixture = fixture;
        }

        [Fact]
        [Trait("Category", "StaticE2E")]
        public void TestGetRole()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = fixture.BASE_URI;

                HttpResponseMessage result = client.GetAsync("/api/role").GetAwaiter().GetResult();

                var content = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                List<RoleDTO> roleDTOs = JsonConvert.DeserializeObject<List<RoleDTO>>(content);

                foreach (var roleDTO in roleDTOs)
                {
                    output.WriteLine($"{roleDTO.Id} {roleDTO.Name}");
                }

                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
                Assert.Equal(3, roleDTOs.Count);
            }
        }

        [Fact]
        [Trait("Category", "StaticE2E")]
        public void TestGetUserInRole()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = fixture.BASE_URI;

                HttpResponseMessage result = client.GetAsync($"/api/role/user/{SeedData.Instance.EngineerRole.Id}").GetAwaiter().GetResult();

                var content = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                List<UserDTO> userDTOs = JsonConvert.DeserializeObject<List<UserDTO>>(content);

                foreach (var userDTO in userDTOs)
                {
                    output.WriteLine($"{userDTO.Id} {userDTO.FirstName} {userDTO.LastName}");
                }

                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            }
        }
    }
}
