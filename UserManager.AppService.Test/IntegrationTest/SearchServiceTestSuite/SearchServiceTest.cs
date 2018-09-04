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
    public class SearchServiceTest : BaseServiceTest, IClassFixture<SearchServiceTestFixture>
    {
        SearchServiceTestFixture fixture;

        public SearchServiceTest(ITestOutputHelper output, SearchServiceTestFixture fixture) : base(output)
        {
            this.fixture = fixture;
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void TestSearchUserWithEmptyQuerry()
        {
            var service = fixture.service;

            QuerryDTO querryDTO = new QuerryDTO();

            var querryResult = service.Search(querryDTO);

            foreach (var userDTO in querryResult)
            {
                _output.WriteLine($"{userDTO.FirstName} {userDTO.LastName}");
            }

            Assert.Equal(4, querryResult.Count());
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void TestSearchUserByName()
        {
            var service = fixture.service;

            QuerryDTO querryDTO = new QuerryDTO()
            {
                Name = "minh"
            };

            var querryResult = service.Search(querryDTO);

            foreach (var userDTO in querryResult)
            {
                _output.WriteLine($"{userDTO.FirstName} {userDTO.LastName}");
            }

            Assert.Equal(2, querryResult.Count());
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void TestSearchUserById()
        {
            var service = fixture.service;

            QuerryDTO querryDTO = new QuerryDTO()
            {
                Id = fixture.TestUser1.Id
            };
            _output.WriteLine(querryDTO.Id);
            var querryResult = service.Search(querryDTO);
            _output.WriteLine(querryResult.Count().ToString());
            foreach (var userDTO in querryResult)
            {
                _output.WriteLine($"{userDTO.FirstName} {userDTO.LastName} {userDTO.Id}");
            }

            Assert.Single(querryResult);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void TestSearchUserByOrganizationName()
        {
            var service = fixture.service;

            #region search by org name "Rosen"
            {
                QuerryDTO querryDTO = new QuerryDTO()
                {
                    OrganizationName = "Rosen"
                };

                var querryResult = service.Search(querryDTO);

                _output.WriteLine("search by organization name: {0}", querryDTO.OrganizationName);
                foreach (var userDTO in querryResult)
                {
                    _output.WriteLine($"{userDTO.FirstName} {userDTO.LastName}");
                }

                Assert.Equal(3, querryResult.Count());
            }
            #endregion

            #region search by org name "UIT"
            {
                QuerryDTO querryDTO = new QuerryDTO()
                {
                    OrganizationName = "UIT"
                };

                var querryResult = service.Search(querryDTO);

                _output.WriteLine("search by organization name: {0}", querryDTO.OrganizationName);
                foreach (var userDTO in querryResult)
                {
                    _output.WriteLine($"{userDTO.FirstName} {userDTO.LastName}");
                }

                Assert.Single(querryResult);
            }
            #endregion
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void TestSearchUserByOrganizationId()
        {
            var service = fixture.service;

            #region search by org name "Rosen"
            {
                QuerryDTO querryDTO = new QuerryDTO()
                {
                    OrganizationId = SeedData.Instance.RosenOrg.Id.ToString()
                };

                var querryResult = service.Search(querryDTO);

                _output.WriteLine("search by organization Id: {0}", querryDTO.OrganizationId);
                foreach (var userDTO in querryResult)
                {
                    _output.WriteLine($"{userDTO.FirstName} {userDTO.LastName}");
                }

                Assert.Equal(3, querryResult.Count());
            }
            #endregion

            #region search by org name "UIT"
            {
                QuerryDTO querryDTO = new QuerryDTO()
                {
                    OrganizationId = SeedData.Instance.UITOrg.Id.ToString()
                };

                var querryResult = service.Search(querryDTO);

                _output.WriteLine("search by organization Id: {0}", querryDTO.OrganizationId);
                foreach (var userDTO in querryResult)
                {
                    _output.WriteLine($"{userDTO.FirstName} {userDTO.LastName}");
                }

                Assert.Single(querryResult);
            }
            #endregion
        }
    }
}
