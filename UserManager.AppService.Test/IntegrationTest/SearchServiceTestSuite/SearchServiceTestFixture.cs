using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Xunit;
using Xunit.Abstractions;

using UserManager.AppService;
using UserManager.AppService.Services;
using UserManager.AppService.Utility;
using UserManager.Contract.DTOs;
using UserManager.Dal;

namespace UserManager.AppService.Test.IntegrationTest.SearchServiceTestSuite
{
    public class SearchServiceTestFixture : IDisposable
    {
        private readonly UserDbContext context;
        public readonly SearchService service;
        public readonly UserDTO TestUser1;
        public readonly UserDTO TestUser2;
        public readonly UserDTO TestUser3;

        public SearchServiceTestFixture()
        {
            var option = new DbContextOptionsBuilder<UserDbContext>()
                        .UseInMemoryDatabase(databaseName: "test_database_search")
                        .Options;

            context = new UserDbContext(option);

            //ensure data is seeded in inmem db
            context.Database.EnsureCreated();

            service = new SearchService(context);

            (TestUser1, TestUser2, TestUser3) = GetTestUser();

            UserService userService = new UserService(context);
            userService.Create(TestUser1, TestUser1.Id);
            userService.Create(TestUser2, TestUser2.Id);
            userService.Create(TestUser3, TestUser3.Id);
        }

        private (UserDTO user1, UserDTO user2, UserDTO user3) GetTestUser()
        {
            SeedData seedData = SeedData.Instance;
            var user1 = new UserDTO()
            {
                Id = "test1",
                FirstName = "minh1",
                LastName = "nguyen le",
                ProfileImage = "image",
                Organization = Mapper.Map(seedData.RosenOrg),
                MainGroup = Mapper.Map(seedData.RosenTechGroup),
                Groups = new[] { Mapper.Map(seedData.RosenTechGroup) },
                MainRole = Mapper.Map(seedData.EngineerRole),
                Roles = new[] { Mapper.Map(seedData.EngineerRole) },
                Email = new Email
                {
                    Main = "main email",
                    Emails = new[] { "main email", "not main email" }
                },
                Phone = new Phone
                {
                    Main = "main phone",
                    Work = new[] { "main phone", "work phone 2" },
                    Private = new[] { "private phone" }
                },
                Mobile = new Mobile
                {
                    Main = "mobile 1",
                    Mobiles = new[] { "mobile 1", "mobile 2" }
                }
            };
            var user2 = new UserDTO()
            {
                Id = "test2",
                FirstName = "minh2",
                LastName = "nguyen le",
                ProfileImage = "image",
                Organization = Mapper.Map(seedData.RosenOrg),
                MainGroup = Mapper.Map(seedData.RosenHRGroup),
                Groups = new[] { Mapper.Map(seedData.RosenHRGroup) },
                MainRole = Mapper.Map(seedData.HRLeadRole),
                Roles = new[] { Mapper.Map(seedData.HRLeadRole) },
                Email = new Email
                {
                    Main = "main email",
                    Emails = new[] { "main email", "not main email" }
                },
                Phone = new Phone
                {
                    Main = "main phone",
                    Work = new[] { "main phone", "work phone 2" },
                    Private = new[] { "private phone" }
                },
                Mobile = new Mobile
                {
                    Main = "mobile 1",
                    Mobiles = new[] { "mobile 1", "mobile 2" }
                }
            };
            var user3 = new UserDTO()
            {
                Id = "test3",
                FirstName = "lan1",
                LastName = "nguyen le",
                ProfileImage = "image",
                Organization = Mapper.Map(seedData.UITOrg),
                MainGroup = Mapper.Map(seedData.UITSEGroup),
                Groups = new[] { Mapper.Map(seedData.UITSEGroup) },
                MainRole = Mapper.Map(seedData.EngineerRole),
                Roles = new[] { Mapper.Map(seedData.EngineerRole) },
                Email = new Email
                {
                    Main = "main email",
                    Emails = new[] { "main email", "not main email" }
                },
                Phone = new Phone
                {
                    Main = "main phone",
                    Work = new[] { "main phone", "work phone 2" },
                    Private = new[] { "private phone" }
                },
                Mobile = new Mobile
                {
                    Main = "mobile 1",
                    Mobiles = new[] { "mobile 1", "mobile 2" }
                }
            };

            return (user1, user2, user3);
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
