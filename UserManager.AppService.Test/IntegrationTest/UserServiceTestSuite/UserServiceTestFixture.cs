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
        public readonly FrontendUserDTO TestCreateUserDTO;

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

            TestCreateUserDTO = GetTestCreateUser();

            service.Create(TestCreateUserDTO);
        }

        public void Dispose()
        {
            //context.Dispose();
        }

        //generate a test user DTO
        private UserDTO GetTestUser()
        {
            SeedData seedData = SeedData.Instance;
            Group group = seedData.RosenTechGroup;
            group.Organization = seedData.RosenOrg;
            return new UserDTO()
            {
                Id = "test1",
                FirstName = "first",
                LastName = "last",
                ProfileImage = "image",
                Organization = Mapper.Map(seedData.RosenOrg),
                MainGroup = Mapper.Map(group),
                Groups = new[] { Mapper.Map(group) },
                MainRole = Mapper.Map(seedData.EngineerRole),
                Roles = new[] { Mapper.Map(seedData.EngineerRole) },
                Email = new Email[]
                {
                    new Email{ Address="main email", IsMain=true },
                    new Email{ Address="not main email", IsMain=false },
                },
                WorkPhone = new Phone[]
                {
                    new Phone{ Number = "main work phone", IsMain = true},
                    new Phone{ Number = "not main work phone", IsMain = false},
                },
                PrivatePhone = new Phone[]
                {
                    new Phone{ Number = "private phone", IsMain = false},
                    new Phone{ Number = "private phone 2", IsMain = false},
                },
                Mobile = new Mobile[]
                {
                    new Mobile{ Number = "main mobile",IsMain = true},
                    new Mobile{ Number = "not main mobile",IsMain = false},
                }
            };
        }

        //generate a test user DTO
        private FrontendUserDTO GetTestCreateUser()
        {
            SeedData seedData = SeedData.Instance;
            return new FrontendUserDTO()
            {
                Id = "test1",
                FirstName = "first",
                LastName = "last",
                ProfileImage = "image",
                OrganizationId = seedData.RosenOrg.Id,
                Groups = new[] { new FrontendGroupDTO(seedData.RosenTechGroup.Id, true) },
                Roles = new[] { new FrontendRoleDTO(seedData.EngineerRole.Id, true) },
                Email = new Email[]
                {
                    new Email{ Address="main email", IsMain=true },
                    new Email{ Address="not main email", IsMain=false },
                },
                WorkPhone = new Phone[]
                {
                    new Phone{ Number = "main work phone", IsMain = true},
                    new Phone{ Number = "not main work phone", IsMain = false},
                },
                PrivatePhone = new Phone[]
                {
                    new Phone{ Number = "private phone", IsMain = false},
                    new Phone{ Number = "private phone 2", IsMain = false},
                },
                Mobile = new Mobile[]
                {
                    new Mobile{ Number = "main mobile",IsMain = true},
                    new Mobile{ Number = "not main mobile",IsMain = false},
                }
            };
        }
    }
}