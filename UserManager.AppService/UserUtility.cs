using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using UserManager.AppService.Utility;
using UserManager.Contract.DTOs;
using UserManager.Dal;

namespace UserManager.AppService
{
    public static class UserUtility
    {
        public static IEnumerable<UserDTO> MapToDTO(this IEnumerable<User> users)
        {
            return users.Select(user => Mapper.MapLight(user));
        }

        public static IEnumerable<UserDTO> ResolveGroupAndRole(this IEnumerable<UserDTO> users, UserDbContext context)
        {
            return users.Select(user =>
            {
                var groups =
                    from gr in context.Groups.Include(g => g.Organization)
                    join usgr in context.UserGroups.Include(ug => ug.User) on gr.Id equals usgr.GroupId
                    where usgr.User.PersonalId.Equals(user.Id)
                    select new { Group = gr, isMain = usgr.IsMain };

                user.Groups = groups.Select(group => Mapper.Map(group.Group)).ToArray();

                var mainGroup = groups.First(g => g.isMain).Group;
                user.MainGroup = Mapper.Map(mainGroup);

                var roles =
                    from role in context.Roles
                    join usrl in context.UserRoles.Include(ur => ur.User) on role.Id equals usrl.RoleId
                    where usrl.User.PersonalId.Equals(user.Id)
                    select new { role, isMain = usrl.IsMain };

                user.Roles = roles.Select(role => Mapper.Map(role.role)).ToArray();

                var mainRole = roles.First(r => r.isMain).role;
                user.MainRole = Mapper.Map(mainRole);

                return user;
            });
        }
    }
}
