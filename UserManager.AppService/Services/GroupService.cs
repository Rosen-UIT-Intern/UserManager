using System;
using System.Collections.Generic;
using System.Linq;

using UserManager.Contract;
using UserManager.Contract.DTOs;
using UserManager.Dal;
using UserManager.AppService.Utility;
using Microsoft.EntityFrameworkCore;

namespace UserManager.AppService.Services
{
    public class GroupService : IGroupService
    {
        private readonly UserDbContext _context;

        public GroupService(UserDbContext context)
        {
            _context = context;
        }

        public IEnumerable<GroupDTO> GetAllGroup()
        {
            return _context.Groups.Select(g => Mapper.Map(g));
        }

        public IEnumerable<GroupDTO> GetAllGroupBelongToOrganization(Guid organizationId)
        {
            return
                from grp in _context.Groups
                where grp.OrganizationId.Equals(organizationId)
                select Mapper.Map(grp)
                ;
        }

        public GroupDTO GetGroup(Guid groupId)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Id.Equals(groupId));

            if (group == null)
            {
                return null;
            }

            return Mapper.Map(group);
        }

        public IEnumerable<UserDTO> GetUsers(Guid groupId)
        {
            var groups = _context.Groups
                .Include(grps => grps.UserGroups)
                    .ThenInclude(usgr => usgr.User)
                    .ThenInclude(usr => usr.Organization)
                .Include(grps => grps.UserGroups)
                    .ThenInclude(usgr => usgr.User)
                    .ThenInclude(usr => usr.UserRoles)
                    .ThenInclude(usrl => usrl.Role)
                .FirstOrDefault(gr => gr.Id.Equals(groupId));

            if (groups == null)
            {
                return null;
            }

            return groups.UserGroups.Select(usgr => usgr.User)
                    .Select(user => Mapper.Map(user))
                    //.MapToDTO().ResolveGroupAndRole(_context)
                    ;
        }
    }
}
