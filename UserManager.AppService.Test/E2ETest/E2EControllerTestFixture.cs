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
    public class E2EControllerTestFixture : IDisposable
    {
        public const string BASE_URL = "http://localhost:5001";
        public readonly Uri BASE_URI = new Uri(BASE_URL);

        public readonly IWebHost _webhost;

        public E2EControllerTestFixture()
        {
            var assemblyName = typeof(Startup.Startup).GetTypeInfo().Assembly.FullName;

            _webhost = WebHost.CreateDefaultBuilder(null)
                              .UseStartup(assemblyName)
                              .UseEnvironment("Development")
                              .UseKestrel()
                              .UseUrls(BASE_URL)
                              .Build();

            _webhost.Start();
        }

        public void Dispose()
        {
            _webhost?.Dispose();
        }
    }
}
