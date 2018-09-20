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
        public static IEnumerable<UserDTO> MapFullWithoutResolvingGroupAndRole(this IEnumerable<User> users)
        {
            return users.Select(user => Mapper.MapFullWithoutResolvingGroupAndRole(user));
        }

        public static IEnumerable<UserDTO> MapOnlyMainWithoutResolvingGroupAndRole(this IEnumerable<User> users)
        {
            return users.Select(user => Mapper.MapOnlyMainWithoutResolvingGroupAndRole(user));
        }

        public static IEnumerable<UserDTO> ResolveGroupAndRole(this IEnumerable<UserDTO> users, UserDbContext context)
        {
            return users.Select(user =>
            {
                user.Groups =
                    (
                    from gr in context.Groups.Include(g => g.Organization)
                    join usgr in context.UserGroups.Include(ug => ug.User) on gr.Id equals usgr.GroupId
                    where usgr.User.PersonalId.Equals(user.Id)
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

        public static IEnumerable<LightUserDTO> ResolveOnlyMainGroupAndRole(this IEnumerable<UserDTO> users, UserDbContext context, bool isMainOnly = false)
        {
            return users.Select(user =>
            {
                var lightUser = new LightUserDTO()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ProfileImage = user.ProfileImage,

                    Organization = user.Organization,

                    MainEmail = user.Email.FirstOrDefault(),
                    MainWorkPhone = user.WorkPhone.FirstOrDefault(),
                    MainPrivatePhone = user.PrivatePhone.FirstOrDefault(),
                    MainMobile = user.Mobile.FirstOrDefault()
                };

                lightUser.MainGroup =
                    (
                    from gr in context.Groups.Include(g => g.Organization)
                    join usgr in context.UserGroups.Include(ug => ug.User) on gr.Id equals usgr.GroupId
                    where usgr.User.PersonalId.Equals(user.Id) && usgr.IsMain
                    select new GroupDTO
                    {
                        Id = gr.Id,
                        Name = gr.Name,
                        Organization = Mapper.Map(gr.Organization),
                        IsMain = usgr.IsMain
                    }
                    ).FirstOrDefault(rldto => rldto.IsMain);

                lightUser.MainRole =
                    (
                    from role in context.Roles
                    join usrl in context.UserRoles.Include(ur => ur.User) on role.Id equals usrl.RoleId
                    where usrl.User.PersonalId.Equals(user.Id) && usrl.IsMain
                    select new RoleDTO
                    {
                        Id = role.Id,
                        Name = role.Name,
                        IsMain = usrl.IsMain
                    }
                    ).FirstOrDefault(rldto => rldto.IsMain);

                return lightUser;
            });
        }
    }
}
