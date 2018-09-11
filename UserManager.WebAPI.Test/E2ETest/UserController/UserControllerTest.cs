using System;
using System.Net;
using System.Reflection;
using System.Net.Http;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Xunit;
using Xunit.Abstractions;

using Newtonsoft.Json;

using UserManager.WebApi;
using UserManager.Contract.DTOs;
using UserManager.Dal;

using UserManager.AppService.Test.E2ETest;

namespace UserManager.AppService.Test.E2ETest.UserController
{
    public class UserControllerTest : E2EControllerTestFixture
    {
        private readonly ITestOutputHelper output;

        public UserControllerTest(ITestOutputHelper output) : base(5002)
        {
            this.output = output;
        }

        [Fact]
        [Trait("Category", "UserE2E")]
        public void TestCreateAndDeleteUserController()
        {
            Init(5002);

            var seed = SeedData.Instance;
            var newUserId = "00000";
            CreateUserDTO newUser = new CreateUserDTO()
            {
                FirstName = "first name",
                LastName = "last name",
                ProfileImage = "image",
                OrganizationId = seed.RosenOrg.Id,
                Groups = new[] { (seed.RosenTechGroup.Id, true) },
                Roles = new[] { (seed.EngineerRole.Id, true) },
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

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = BASE_URI;

                HttpResponseMessage result = client.PostAsJsonAsync("/api/user", newUser).GetAwaiter().GetResult();

                var userId = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
                output.WriteLine("created user's id: {0}", userId);

                var deleteResult = client.DeleteAsync($"/api/user/{userId}").GetAwaiter().GetResult();
                Assert.Equal(HttpStatusCode.OK, deleteResult.StatusCode);
            }
        }

        //[Fact]
        //[Trait("Category", "UserE2E")]
        //public void TestDeleteUserController()
        //{
        //    Init(5003);

        //    var seed = SeedData.Instance;
        //    var newUserId = "00000";
        //    CreateUserDTO newUser = new CreateUserDTO()
        //    {
        //        FirstName = "first name",
        //        LastName = "last name",
        //        ProfileImage = "image",
        //        OrganizationId = seed.RosenOrg.Id,
        //        Groups = new[] { (seed.RosenTechGroup.Id, true) },
        //        Roles = new[] { (seed.EngineerRole.Id, true) },
        //        Email = new Email[]
        //        {
        //            new Email{ Address="main email", IsMain=true},
        //            new Email{ Address="not main email", IsMain=false}
        //        },
        //        WorkPhone = new Phone[]
        //        {
        //            new Phone{ Number="main work phone",IsMain=true},
        //            new Phone{ Number="not main work phone",IsMain=false},
        //        },
        //        PrivatePhone = new Phone[]
        //        {
        //            new Phone{ Number="private phone 1",IsMain=false },
        //            new Phone{ Number="private phone 2",IsMain=false },
        //        },
        //        Mobile = new Mobile[]
        //        {
        //            new Mobile{ Number="main mobile",IsMain =true },
        //            new Mobile{ Number="not main mobile",IsMain =false },
        //        }
        //    };

        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = BASE_URI;

        //        HttpResponseMessage result = client.PostAsJsonAsync("/api/user", newUser).GetAwaiter().GetResult();

        //        var userId = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();



        //        var deleteResult = client.DeleteAsync($"/api/user/{userId}").GetAwaiter().GetResult();
        //        Assert.Equal(HttpStatusCode.OK, deleteResult.StatusCode);
        //    }
        //}
    }
}
