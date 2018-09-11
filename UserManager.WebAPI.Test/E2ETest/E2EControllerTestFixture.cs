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
        public string BASE_URL { get; private set; }
        public Uri BASE_URI { get; private set; }

        public IWebHost _webhost { get; private set; }

        public E2EControllerTestFixture(int NoInit)
        {
            _webhost = null;
        }

        public E2EControllerTestFixture()
        {
            Init(5000);
        }

        protected void Init(int port)
        {
            BASE_URL = $"http://localhost:{port}";
            BASE_URI = new Uri(BASE_URL);

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
