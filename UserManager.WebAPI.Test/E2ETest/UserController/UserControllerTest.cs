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
        private readonly ITestOutputHelper _output;

        public UserControllerTest(ITestOutputHelper output) : base(5002)
        {
            _output = output;
        }

        [Fact]
        [Trait("Category", "UserE2E")]
        public void TestCreateUserController()
        {
            Init(5002);

            var seed = SeedData.Instance;
            FrontendUserDTO newUser = new FrontendUserDTO()
            {
                Id = "creat",
                FirstName = "first name",
                LastName = "last name",
                ProfileImage = "image",
                OrganizationId = seed.RosenOrg.Id,
                Groups = new[] { new FrontendGroupDTO(seed.RosenTechGroup.Id, true) },
                Roles = new[] { new FrontendRoleDTO(seed.EngineerRole.Id, true) },
                Email = new Email[]
                {
                    new Email{ Address="main_email@email.com", IsMain=true},
                    new Email{ Address="not_main_email@email.com", IsMain=false}
                },
                WorkPhone = new Phone[]
                {
                    new Phone{ Number="+8411111111",IsMain=true},
                    new Phone{ Number="+8422222222",IsMain=false},
                },
                PrivatePhone = new Phone[]
                {
                    new Phone{ Number="+8433333333",IsMain=false },
                    new Phone{ Number="+8444444444",IsMain=false },
                },
                Mobile = new Mobile[]
                {
                    new Mobile{ Number="+8455555555",IsMain =true },
                    new Mobile{ Number="+8466666666",IsMain =false },
                }
            };

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = BASE_URI;

                HttpResponseMessage result = client.PostAsJsonAsync("/api/user", newUser).GetAwaiter().GetResult();

                var userId = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                _output.WriteLine("created user's id: {0}", userId);
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
                {
                    HttpResponseMessage verifyResult = client.GetAsync($"/api/user/{newUser.Id}").GetAwaiter().GetResult();

                    var user = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    _output.WriteLine(user);
                }

                var delResult = client.DeleteAsync($"/api/user/{userId}").GetAwaiter().GetResult();
                _output.WriteLine(delResult.StatusCode.ToString());
                Assert.Equal(HttpStatusCode.OK, delResult.StatusCode);
            }
        }

        [Fact]
        [Trait("Category", "UserE2E")]
        public void TestDeleteUserController()
        {
            Init(5003);

            var seed = SeedData.Instance;
            FrontendUserDTO newUser = new FrontendUserDTO()
            {
                Id = "delet",
                FirstName = "first name",
                LastName = "last name",
                ProfileImage = "image",
                OrganizationId = seed.RosenOrg.Id,
                Groups = new[] { new FrontendGroupDTO(seed.RosenTechGroup.Id, true) },
                Roles = new[] { new FrontendRoleDTO(seed.EngineerRole.Id, true) },
                Email = new Email[]
                {
                    new Email{ Address="main_email@email.com", IsMain=true},
                    new Email{ Address="not_main_email@email.com", IsMain=false}
                },
                WorkPhone = new Phone[]
                {
                    new Phone{ Number="+8411111111",IsMain=true},
                    new Phone{ Number="+8422222222",IsMain=false},
                },
                PrivatePhone = new Phone[]
                {
                    new Phone{ Number="+8433333333",IsMain=false },
                    new Phone{ Number="+8444444444",IsMain=false },
                },
                Mobile = new Mobile[]
                {
                    new Mobile{ Number="+8455555555",IsMain =true },
                    new Mobile{ Number="+8466666666",IsMain =false },
                }
            };

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = BASE_URI;

                HttpResponseMessage result = client.PostAsJsonAsync("/api/user", newUser).GetAwaiter().GetResult();

                var userId = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _output.WriteLine(userId);
                var deleteResult = client.DeleteAsync($"/api/user/{userId}").GetAwaiter().GetResult();
                Assert.Equal(HttpStatusCode.OK, deleteResult.StatusCode);
            }
        }
    }
}
