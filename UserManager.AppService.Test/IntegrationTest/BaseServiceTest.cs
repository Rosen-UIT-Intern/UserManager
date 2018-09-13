using Microsoft.EntityFrameworkCore;
using UserManager.Dal;
using System;
using System.Collections.Generic;
using System.Text;

using Xunit;
using Xunit.Abstractions;

namespace UserManager.AppService.Test.IntegrationTest
{
    public class BaseServiceTest
    {
        protected readonly ITestOutputHelper _output;

        public BaseServiceTest(ITestOutputHelper output)
        {
            _output = output;
        }

        protected UserDbContext InitDbContext(string dbName = "")
        {
            var option = new DbContextOptionsBuilder<UserDbContext>()
                        .UseInMemoryDatabase(databaseName: dbName)
                        .EnableSensitiveDataLogging(true)
                        .Options;

            var context = new UserDbContext(option);

            //ensure data is seeded in inmem db
            context.Database.EnsureCreated();

            return context;
        }
    }
}
