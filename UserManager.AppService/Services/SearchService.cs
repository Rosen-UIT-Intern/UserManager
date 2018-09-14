using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using UserManager.Contract;
using UserManager.Contract.DTOs;
using UserManager.Dal;
using UserManager.AppService.Utility;

namespace UserManager.AppService.Services
{
    public static class UserFilter
    {
        public static IEnumerable<User> FilterByName(this IEnumerable<User> users, QuerryDTO querry)
        {
            return
                string.IsNullOrWhiteSpace(querry.Name)
                ?
                users
                :
                from user in users
                where
                    $"{user.FirstName} {user.LastName}".Contains(querry.Name)
                select user
                ;
        }
        public static IEnumerable<User> FilterById(this IEnumerable<User> users, QuerryDTO querry)
        {
            return
                string.IsNullOrWhiteSpace(querry.Id)
                ?
                users
                :
                from user in users
                where
                    user.PersonalId.Contains(querry.Id)
                select user
                ;
        }
        public static IEnumerable<User> FilterByOrganizationName(this IEnumerable<User> users, QuerryDTO querry)
        {
            return
                string.IsNullOrWhiteSpace(querry.OrganizationName)
                ?
                users
                :
                from user in users
                where user.Organization.Name.Contains(querry.OrganizationName)
                select user
                ;
        }
        public static IEnumerable<User> FilterByOrganizationId(this IEnumerable<User> users, QuerryDTO querry)
        {
            return
                querry.OrganizationId == default(Guid) ?
                users
                :
                from user in users
                where user.OrganizationId.Equals(querry.OrganizationId)
                select user
                ;
        }
    }

    public class SearchService : ISearchService
    {
        private readonly UserDbContext _context;

        public SearchService(UserDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserDTO> Search(QuerryDTO querry)
        {
            return _context.Users.Include(u => u.Organization)
                .FilterByOrganizationId(querry)
                .FilterByOrganizationName(querry)
                .FilterByName(querry)
                .FilterById(querry)
                .MapToDTO().ResolveGroupAndRole(_context);
        }
    }
}
