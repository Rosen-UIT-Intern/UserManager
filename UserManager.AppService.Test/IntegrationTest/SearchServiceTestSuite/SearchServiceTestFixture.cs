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
        public readonly CreateUserDTO TestUser1;
        public readonly CreateUserDTO TestUser2;
        public readonly CreateUserDTO TestUser3;
        public readonly string TestUser1Id = "test1";
        public readonly string TestUser2Id = "test2";
        public readonly string TestUser3Id = "test3";

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
            userService.Create(TestUser1, TestUser1Id);
            userService.Create(TestUser2, TestUser2Id);
            userService.Create(TestUser3, TestUser3Id);
        }

        private (CreateUserDTO user1, CreateUserDTO user2, CreateUserDTO user3) GetTestUser()
        {
            SeedData seedData = SeedData.Instance;

            var user1 = new CreateUserDTO()
            {
                FirstName = "minh1",
                LastName = "nguyen le",
                ProfileImage = "image",
                OrganizationId = seedData.RosenOrg.Id,
                Groups = new[] { (seedData.RosenTechGroup.Id, true) },
                Roles = new[] { (seedData.EngineerRole.Id, true) },
                Email = new Email[]
                {
                    new Email{ Address="main email", IsMain=true},
                    new Email{ Address="not main email", IsMain=false}
                },
                WorkPhone = new Phone[]
                {
                    new Phone{ Number="main work phone",IsMain=true},
                    new Phone{ Number="not main work phone",IsMain=false},
                },
                PrivatePhone = new Phone[]
                {
                    new Phone{ Number="private phone 1",IsMain=false },
                    new Phone{ Number="private phone 2",IsMain=false },
                },
                Mobile = new Mobile[]
                {
                    new Mobile{ Number="main mobile",IsMain =true },
                    new Mobile{ Number="not main mobile",IsMain =false },
                }
            };
            var user2 = new CreateUserDTO()
            {
                FirstName = "minh2",
                LastName = "nguyen le",
                ProfileImage = "image",
                OrganizationId = seedData.RosenOrg.Id,
                Groups = new[] { (seedData.RosenHRGroup.Id, true) },
                Roles = new[] { (seedData.HRLeadRole.Id, true) },
                Email = new Email[]
                {
                    new Email{ Address="main email", IsMain=true},
                    new Email{ Address="not main email", IsMain=false}
                },
                WorkPhone = new Phone[]
                {
                    new Phone{ Number="main work phone",IsMain=true},
                    new Phone{ Number="not main work phone",IsMain=false},
                },
                PrivatePhone = new Phone[]
                {
                    new Phone{ Number="private phone 1",IsMain=false },
                    new Phone{ Number="private phone 2",IsMain=false },
                },
                Mobile = new Mobile[]
                {
                    new Mobile{ Number="main mobile",IsMain =true },
                    new Mobile{ Number="not main mobile",IsMain =false },
                }
            };
            var user3 = new CreateUserDTO()
            {
                FirstName = "lan1",
                LastName = "nguyen le",
                ProfileImage = "image",
                OrganizationId = seedData.UITOrg.Id,
                Groups = new[] { (seedData.UITSEGroup.Id, true) },
                Roles = new[] { (seedData.EngineerRole.Id, true) },
                Email = new Email[]
                {
                    new Email{ Address="main email", IsMain=true},
                    new Email{ Address="not main email", IsMain=false}
                },
                WorkPhone = new Phone[]
                {
                    new Phone{ Number="main work phone",IsMain=true},
                    new Phone{ Number="not main work phone",IsMain=false},
                },
                PrivatePhone = new Phone[]
                {
                    new Phone{ Number="private phone 1",IsMain=false },
                    new Phone{ Number="private phone 2",IsMain=false },
                },
                Mobile = new Mobile[]
                {
                    new Mobile{ Number="main mobile",IsMain =true },
                    new Mobile{ Number="not main mobile",IsMain =false },
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
