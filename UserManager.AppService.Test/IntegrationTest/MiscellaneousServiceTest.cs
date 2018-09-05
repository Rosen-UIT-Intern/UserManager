using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Xunit;

using UserManager.AppService;
using UserManager.AppService.Services;
using UserManager.AppService.Utility;
using UserManager.Contract.DTOs;
using UserManager.Dal;
using Xunit.Abstractions;

namespace UserManager.AppService.Test.IntegrationTest
{
    public class MiscellaneousServiceTest : BaseServiceTest
    {
        public MiscellaneousServiceTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void TestGetAllRole()
        {
            using (var context = InitDbContext("get_all_roles"))
            {
                //instantiate a role service
                var service = new RoleService(context);

                //test get all roles
                Assert.Equal(3, service.GetRoles().Count());
            }
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void TestGetOneRole()
        {
            using (var context = InitDbContext("get_one_role"))
            {
                //instantiate a role service
                var service = new RoleService(context);

                //get engineer role
                var role = service.GetRole(Guid.Parse("d1eb257f-9a58-4751-8a6d-a1f0ed91b3ba"));

                //test get all roles
                Assert.Equal("Engineer", role.Name);
            }
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void TestGetUserThatHaveRole()
        {
            using (var context = InitDbContext("get_user_that_have_role"))
            {
                //instantiate a role service
                var service = new RoleService(context);

                {
                    //get user that have engineer role
                    var users = service.GetUsers(SeedData.Instance.EngineerRole.Id);

                    //test get all roles
                    Assert.Single(users);
                }

                {
                    //get user that have tech lead role
                    var users = service.GetUsers(SeedData.Instance.TechLeadRole.Id);

                    //test get all roles
                    Assert.Empty(users);
                }
            }
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void TestGetUserThatHaveGroup()
        {
            using (var context = InitDbContext("get_user_that_have_group"))
            {
                //instantiate a role service
                var service = new GroupService(context);

                {
                    //get user that have engineer role
                    var users = service.GetUsers(SeedData.Instance.RosenTechGroup.Id);

                    //test get all roles
                    Assert.Single(users);

                    _output.WriteLine($"{users.First().Id} {users.First().FirstName} {users.First().LastName}");
                }

                {
                    //get user that have tech lead role
                    var users = service.GetUsers(SeedData.Instance.UITSEGroup.Id);

                    //test get all roles
                    Assert.Empty(users);
                }
            }
        }
    }
}
