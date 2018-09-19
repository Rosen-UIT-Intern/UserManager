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
        public static IEnumerable<UserDTO> MapToDTOFull(this IEnumerable<User> users)
        {
            return users.Select(user => Mapper.MapFullWithoutResolvingGroupAndRole(user));
        }

        public static IEnumerable<UserDTO> MapToDTOLight(this IEnumerable<User> users)
        {
            return users.Select(user => Mapper.MapOnlyMainWithoutResolvingGroupAndRole(user));
        }

        public static IEnumerable<UserDTO> ResolveGroupAndRole(this IEnumerable<UserDTO> users, UserDbContext context, bool isMainOnly = false)
        {
            return users.Select(user =>
            {
                user.Groups =
                    (
                    from gr in context.Groups.Include(g => g.Organization)
                    join usgr in context.UserGroups.Include(ug => ug.User) on gr.Id equals usgr.GroupId
                    where usgr.User.PersonalId.Equals(user.Id)
                    //for only resolve main usergroup
                    && (!isMainOnly //skip the next condition if isMainOnly is false
                        || isMainOnly && usgr.IsMain)
                    select new GroupDTO
                    {
                        Id = gr.Id,
                        Name = gr.Name,
                        Organization = Mapper.Map(gr.Organization),
                        IsMain = usgr.IsMain
                    }
                    )
                    .ToArray();

                user.Roles =
                    (
                    from role in context.Roles
                    join usrl in context.UserRoles.Include(ur => ur.User) on role.Id equals usrl.RoleId
                    where usrl.User.PersonalId.Equals(user.Id)
                    //for only resolve main usergroup
                    && (!isMainOnly //skip the next condition if isMainOnly is false
                        || isMainOnly && usrl.IsMain)
                    select new RoleDTO
                    {
                        Id = role.Id,
                        Name = role.Name,
                        IsMain = usrl.IsMain
                    }
                    )
                    .ToArray();

                return user;
            });
        }
    }
}
