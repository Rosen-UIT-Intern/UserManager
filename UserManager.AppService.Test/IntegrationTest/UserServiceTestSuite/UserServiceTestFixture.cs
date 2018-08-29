using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

using Xunit;
using Xunit.Abstractions;

using UserManager.Dal;
using UserManager.AppService.Services;
using UserManager.Contract.DTOs;
using UserManager.AppService.Utility;

namespace UserManager.AppService.Test.IntegrationTest.UserServiceTestSuite
{
    public class UserServiceTestFixture : IDisposable
    {
        private readonly UserDbContext context;
        public readonly UserService service;
        public readonly UserDTO TestUserDTO;

        public UserServiceTestFixture()
        {
            var option = new DbContextOptionsBuilder<UserDbContext>()
                        .UseInMemoryDatabase(databaseName: "test_database")
                        .Options;

            context = new UserDbContext(option);

            //ensure data is seeded in inmem db
            context.Database.EnsureCreated();

            service = new UserService(context);

            TestUserDTO = GetTestUser();
            service.Create(TestUserDTO, TestUserDTO.Id);
        }

        public void Dispose()
        {
            //context.Dispose();
        }

        //generate a test user DTO
        private UserDTO GetTestUser()
        {
            SeedData seedData = SeedData.Instance;
            return new UserDTO()
            {
                Id = "test1",
                FirstName = "first",
                LastName = "last",
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
        }
    }
}