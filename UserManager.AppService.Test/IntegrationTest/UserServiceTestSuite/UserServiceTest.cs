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
using Newtonsoft.Json;

namespace UserManager.AppService.Test.IntegrationTest.UserServiceTestSuite
{
    public class UserServiceTest : BaseServiceTest, IClassFixture<UserServiceTestFixture>
    {
        private readonly UserServiceTestFixture fixture;

        public UserServiceTest(ITestOutputHelper output, UserServiceTestFixture fixture) : base(output)
        {
            this.fixture = fixture;
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void TestCreateUser()
        {
            using (var context = InitDbContext("create_user"))
            {
                var service = new UserService(context);

                //create a user and insert into db
                var createUserDTOId = fixture.TestCreateUserDTO.Id;
                FrontendUserDTO createUserDTO = fixture.TestCreateUserDTO;

                try
                {
                    var newId = service.Create(createUserDTO);
                    Assert.Equal(createUserDTOId, newId);
                    _output.WriteLine(newId);
                }
                catch (ArgumentException aex)
                {
                    Assert.True(false, aex.Message);
                    _output.WriteLine(aex.Message);
                }
            }
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void TestUpdateUser()
        {
            using (var context = InitDbContext("update_user"))
            {
                var service = new UserService(context);
                var seed = SeedData.Instance;
                //create a user and insert into db
                var createUserDTOId = fixture.TestUserDTO.Id;
                FrontendUserDTO createUserDTO = fixture.TestCreateUserDTO;
                var userId = service.Create(createUserDTO);

                //update some user's info
                createUserDTO.FirstName = "changed first name";
                createUserDTO.LastName = "changed last name";
                createUserDTO.Groups = new[] { new FrontendGroupDTO(seed.RosenHRGroup.Id, true) };
                createUserDTO.Roles = new[] { new FrontendRoleDTO(seed.HRLeadRole.Id, true) };

                try
                {
                    var tttId = service.Update(createUserDTO);
                    Assert.Equal(createUserDTOId, tttId);
                    _output.WriteLine(tttId);

                    var updatedUserDTO = service.GetUser(tttId);
                    _output.WriteLine(JsonConvert.SerializeObject(updatedUserDTO));
                }
                catch (ArgumentException aex)
                {
                    _output.WriteLine(aex.Message);
                }
            }
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void TestDeleteUser()
        {
            using (var context = InitDbContext("delete_user"))
            {
                var service = new UserService(context);

                //create a user and insert into db
                //UserDTO userDTO = fixture.TestUserDTO;
                var createUserDTOId = fixture.TestUserDTO.Id;
                FrontendUserDTO createUserDTO = fixture.TestCreateUserDTO;

                service.Create(createUserDTO);

                Assert.True(service.Delete(createUserDTOId));
            }
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void TestGetUser()
        {
            var service = fixture.service;

            UserDTO userDTO = fixture.TestUserDTO;

            //confirm that user is saved correctly in the db
            //and userDTO is generated correctly by UserService
            //get inserted user from db
            var userDTOfromDb = service.GetUser(userDTO.Id);
            Assert.Equal(userDTO.Id, userDTOfromDb.Id);
            Assert.Equal(userDTO.FirstName, userDTOfromDb.FirstName);
            Assert.Equal(userDTO.LastName, userDTOfromDb.LastName);

            //confirm image is saved correctly
            Assert.Equal(userDTO.ProfileImage, userDTOfromDb.ProfileImage);

            //confirm that email,phone and mobile is saved correctly
            Assert.Equal(userDTO.Email, userDTOfromDb.Email);
            Assert.Equal(userDTO.WorkPhone, userDTOfromDb.WorkPhone);
            Assert.Equal(userDTO.PrivatePhone, userDTOfromDb.PrivatePhone);
            Assert.Equal(userDTO.Mobile, userDTOfromDb.Mobile);

            //confirm that user's organization is saved correctly
            Assert.Equal(userDTO.Organization.Id, userDTOfromDb.Organization.Id);

            //confirm that user's group is saved correctly
            Assert.Equal(userDTO.Groups.Length, userDTOfromDb.Groups.Length);

            //confirm that user's role is saved correctly
            Assert.Equal(userDTO.Roles.Length, userDTOfromDb.Roles.Length);
        }
    }
}
