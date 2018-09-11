﻿using System;
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

            Group rosenTechGroup = seedData.RosenTechGroup;
            rosenTechGroup.Organization = seedData.RosenOrg;
            Group rosenHRGroup = seedData.RosenHRGroup;
            rosenHRGroup.Organization = seedData.RosenOrg;
            Group uITSEGroup = seedData.UITSEGroup;
            uITSEGroup.Organization = seedData.UITOrg;

            var user1 = new UserDTO()
            {
                Id = "test1",
                FirstName = "minh1",
                LastName = "nguyen le",
                ProfileImage = "image",
                Organization = Mapper.Map(seedData.RosenOrg),
                MainGroup = Mapper.Map(rosenTechGroup),
                Groups = new[] { Mapper.Map(rosenTechGroup) },
                MainRole = Mapper.Map(seedData.EngineerRole),
                Roles = new[] { Mapper.Map(seedData.EngineerRole) },
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
            var user2 = new UserDTO()
            {
                Id = "test2",
                FirstName = "minh2",
                LastName = "nguyen le",
                ProfileImage = "image",
                Organization = Mapper.Map(seedData.RosenOrg),
                MainGroup = Mapper.Map(rosenHRGroup),
                Groups = new[] { Mapper.Map(rosenHRGroup) },
                MainRole = Mapper.Map(seedData.HRLeadRole),
                Roles = new[] { Mapper.Map(seedData.HRLeadRole) },
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
            var user3 = new UserDTO()
            {
                Id = "test3",
                FirstName = "lan1",
                LastName = "nguyen le",
                ProfileImage = "image",
                Organization = Mapper.Map(seedData.UITOrg),
                MainGroup = Mapper.Map(uITSEGroup),
                Groups = new[] { Mapper.Map(uITSEGroup) },
                MainRole = Mapper.Map(seedData.EngineerRole),
                Roles = new[] { Mapper.Map(seedData.EngineerRole) },
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
