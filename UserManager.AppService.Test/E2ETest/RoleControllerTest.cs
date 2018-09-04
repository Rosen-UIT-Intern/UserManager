using System;
using System.Net;
using System.Reflection;
using System.Net.Http;
using System.Collections.Generic;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

using Xunit;
using Xunit.Abstractions;

using Newtonsoft.Json;

using UserManager.Contract.DTOs;
using UserManager.WebApi.Controllers;

namespace UserManager.AppService.Test.E2ETest
{
    public class RoleControllerTest : IDisposable
    {
        private const string BASE_URL = "http://localhost:5001";
        private readonly Uri BASE_URI = new Uri(BASE_URL);

        private readonly IWebHost _webhost;
        protected readonly ITestOutputHelper _output;

        public RoleControllerTest(ITestOutputHelper output)
        {
            var assemblyName = typeof(Startup.Startup).GetTypeInfo().Assembly.FullName;

            _output = output;
            _webhost = WebHost.CreateDefaultBuilder(null)
                              .UseStartup(assemblyName)
                              .UseEnvironment("Development")
                              .UseKestrel()
                              .UseUrls(BASE_URL)
                              .Build();

            _webhost.Start();

            RoleController roleController = new RoleController(null);
        }

        [Fact]
        [Trait("Category", "E2E")]
        public void TestGetRole()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = BASE_URI;

                HttpResponseMessage result = client.GetAsync("/api/role").GetAwaiter().GetResult();

                var content = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                List<RoleDTO> roleDTOs = JsonConvert.DeserializeObject<List<RoleDTO>>(content);

                foreach (var roleDTO in roleDTOs)
                {
                    _output.WriteLine($"{roleDTO.Id} {roleDTO.Name}");
                }

                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            }
        }

        [Fact]
        public void TestGetOrganization()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = BASE_URI;

                HttpResponseMessage result = client.GetAsync("/api/role").GetAwaiter().GetResult();

                var content = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                List<RoleDTO> roleDTOs = JsonConvert.DeserializeObject<List<RoleDTO>>(content);

                foreach (var roleDTO in roleDTOs)
                {
                    _output.WriteLine($"{roleDTO.Id} {roleDTO.Name}");
                }

                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            }
        }

        public void Dispose()
        {
            _webhost?.Dispose();
        }
    }
}
