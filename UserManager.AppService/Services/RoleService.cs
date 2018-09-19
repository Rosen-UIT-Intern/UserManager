using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using UserManager.Contract;
using UserManager.Contract.DTOs;
using UserManager.Dal;
using UserManager.AppService.Utility;

namespace UserManager.AppService.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserDbContext _context;

        public RoleService(UserDbContext context)
        {
            _context = context;
        }

        public RoleDTO GetRole(Guid roleId)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Id.Equals(roleId));

            if (role == null)
            {
                return null;
            }

            return Mapper.Map(role);
        }

        public IEnumerable<RoleDTO> GetRoles()
        {
            return _context.Roles.Select(r => Mapper.Map(r));
        }

        public IEnumerable<UserDTO> GetUsers(Guid roleId)
        {
            var role = _context.Roles
                .Include(r => r.UserRoles)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Organization)
                .Include(r => r.UserRoles)
                    .ThenInclude(usrl => usrl.User)
                    .ThenInclude(usr => usr.UserGroups)
                    .ThenInclude(usgr => usgr.Group)
                .FirstOrDefault(r => r.Id.Equals(roleId));

            if (role == null)
            {
                return null;
            }

            return role.UserRoles.Select(usrl => usrl.User)
                    .MapToDTOFull().ResolveGroupAndRole(_context)
                    ;
        }
    }
}
