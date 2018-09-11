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
    public class GroupControllerTest
    {
        private readonly E2EControllerTestFixture fixture;
        private readonly ITestOutputHelper output;

        public GroupControllerTest(E2EControllerTestFixture fixture, ITestOutputHelper output)
        {
            this.output = output;
            this.fixture = fixture;
        }

        [Fact]
        [Trait("Category", "StaticE2E")]
        public void TestGetGroup()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = fixture.BASE_URI;

                HttpResponseMessage result = client.GetAsync("/api/group").GetAwaiter().GetResult();

                var content = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                List<GroupDTO> groupDTOs = JsonConvert.DeserializeObject<List<GroupDTO>>(content);

                foreach (var groupDTO in groupDTOs)
                {
                    output.WriteLine($"{groupDTO.Id} {groupDTO.Name}");
                }

                Assert.Equal(HttpStatusCode.OK, result.StatusCode);

                Assert.Equal(4, groupDTOs.Count);
            }
        }

        [Fact]
        [Trait("Category", "StaticE2E")]
        public void TestGetGroupBelongToOrganization()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = fixture.BASE_URI;

                HttpResponseMessage result = client.GetAsync($"/api/group/org/{SeedData.Instance.RosenOrg.Id}").GetAwaiter().GetResult();

                var content = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                List<GroupDTO> groupDTOs = JsonConvert.DeserializeObject<List<GroupDTO>>(content);

                foreach (var groupDTO in groupDTOs)
                {
                    output.WriteLine($"{groupDTO.Id} {groupDTO.Name}");
                }

                Assert.Equal(HttpStatusCode.OK, result.StatusCode);

                Assert.Equal(2, groupDTOs.Count);
            }
        }

        [Fact]
        [Trait("Category", "StaticE2E")]
        public void TestGetUserBelongToGroup()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = fixture.BASE_URI;

                HttpResponseMessage result = client.GetAsync($"/api/group/user/{SeedData.Instance.RosenTechGroup.Id}").GetAwaiter().GetResult();

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
