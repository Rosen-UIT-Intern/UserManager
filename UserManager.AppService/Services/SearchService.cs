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
            if (string.IsNullOrWhiteSpace(querry.Name))
            {
                return users;
            }
            else
            {
                return
                from user in users
                where
                    $"{user.FirstName} {user.LastName}".Contains(querry.Name)
                select user
                ;
            }
        }
        public static IEnumerable<User> FilterById(this IEnumerable<User> users, QuerryDTO querry)
        {
            if (string.IsNullOrWhiteSpace(querry.Id))
            {
                return users;
            }
            else
            {
                return
                from user in users
                where
                    user.PersonalId.Contains(querry.Id)
                select user
                ;
            }
        }

        public static IEnumerable<User> FilterByOrganizationName(this IEnumerable<User> users, QuerryDTO querry)
        {
            if (string.IsNullOrWhiteSpace(querry.OrganizationName))
            {
                return users;
            }
            else
            {
                return
                from user in users
                where user.Organization.Name.Contains(querry.OrganizationName)
                select user
                ;
            }
        }
        public static IEnumerable<User> FilterByOrganizationId(this IEnumerable<User> users, QuerryDTO querry)
        {
            if (querry.OrganizationId == default(Guid))
            {
                return users;
            }
            else
            {
                return
                from user in users
                where user.OrganizationId.Equals(querry.OrganizationId)
                select user
                ;
            }
        }

        public static IEnumerable<User> FilterByGroupName(this IEnumerable<User> users, QuerryDTO querry)
        {
            if (string.IsNullOrWhiteSpace(querry.GroupName))
            {
                return users;
            }
            else
            {
                return
                from user in users
                where user.UserGroups.Any(usrl => usrl.Group.Name.Contains(querry.GroupName))
                select user
                ;
            }
        }
        public static IEnumerable<User> FilterByGroupId(this IEnumerable<User> users, QuerryDTO querry)
        {
            if (querry.GroupId.Equals(default(Guid)))
            {
                return users;
            }
            else
            {
                return
                from user in users
                where user.UserGroups.Any(usgr => usgr.GroupId.Equals(querry.GroupId))
                select user
                ;
            }
        }

        public static IEnumerable<User> FilterByRoleName(this IEnumerable<User> users, QuerryDTO querry)
        {
            if (string.IsNullOrWhiteSpace(querry.RoleName))
            {
                return users;
            }
            else
            {
                return
                from user in users
                where user.UserRoles.Any(usrl => usrl.Role.Name.Contains(querry.RoleName))
                select user
                ;
            }
        }
        public static IEnumerable<User> FilterByRoleId(this IEnumerable<User> users, QuerryDTO querry)
        {
            if (querry.RoleId.Equals(default(Guid)))
            {
                return users;
            }
            else
            {
                return
                from user in users
                where user.UserRoles.Any(usrl => usrl.RoleId.Equals(querry.RoleId))
                select user
                ;
            }
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
            return
                _context.Users
                    .Include(u => u.Organization)
                    .Include(u => u.UserGroups)
                        .ThenInclude(usgr => usgr.Group)
                            .ThenInclude(grp => grp.Organization)
                    .Include(u => u.UserRoles)
                        .ThenInclude(usrl => usrl.Role)
                .FilterByOrganizationId(querry)
                .FilterByOrganizationName(querry)
                .FilterByGroupId(querry)
                .FilterByGroupName(querry)
                .FilterByRoleId(querry)
                .FilterByRoleName(querry)
                .FilterByName(querry)
                .FilterById(querry)
                .MapToDTO().ResolveGroupAndRole(_context);
        }
    }
}
