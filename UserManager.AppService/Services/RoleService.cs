using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}
