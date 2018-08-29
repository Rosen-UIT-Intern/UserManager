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

            #region search by org name "Uit"
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
    }
}
