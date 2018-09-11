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

using UserManager.AppService.Test.E2ETest;

namespace UserManager.AppService.Test.StaticE2ETest
{
    [Collection("StaticE2E")]
    public class OrganizationControllerTest
    {
        private readonly E2EControllerTestFixture fixture;
        private readonly ITestOutputHelper output;

        public OrganizationControllerTest(ITestOutputHelper output, E2EControllerTestFixture fixture)
        {
            this.output = output;
            this.fixture = fixture;
        }

        [Fact]
        [Trait("Category", "StaticE2E")]
        public void TestGetOrganization()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = fixture.BASE_URI;

                HttpResponseMessage result = client.GetAsync("/api/organization").GetAwaiter().GetResult();

                var content = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                List<OrganizationDTO> orgDTOs = JsonConvert.DeserializeObject<List<OrganizationDTO>>(content);

                foreach (var orgDTO in orgDTOs)
                {
                    output.WriteLine($"{orgDTO.Id} {orgDTO.Name}");
                }

                Assert.Equal(HttpStatusCode.OK, result.StatusCode);

                Assert.Equal(2, orgDTOs.Count);
            }
        }
    }
}
