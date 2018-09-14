﻿using System;
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
    public class UserService : IUserService
    {
        private readonly UserDbContext _context;

        public UserService(UserDbContext context)
        {
            _context = context;
        }

        public ICollection<UserDTO> GetUsers()
        {
            return _context.Users.Include(u => u.Organization).MapToDTO().ResolveGroupAndRole(_context).ToList();
        }

        public ICollection<UserDTO> GetLightUsers()
        {
            return _context.Users.Include(u => u.Organization).MapToDTO().ToList();
        }

        public UserDTO GetUser(string id)
        {
            var users = (
                from usr in _context.Users.Include(u => u.Organization)
                where usr.PersonalId.Equals(id)
                select new UserDTO
                {
                    Id = usr.PersonalId,
                    FirstName = usr.FirstName,
                    LastName = usr.LastName,
                    ProfileImage = usr.ProfileImage,
                    Organization = Mapper.Map(usr.Organization),
                    Email = JsonConvert.DeserializeObject<Email[]>(usr.Email),
                    WorkPhone = JsonConvert.DeserializeObject<Phone[]>(usr.WorkPhone),
                    PrivatePhone = JsonConvert.DeserializeObject<Phone[]>(usr.PrivatePhone),
                    Mobile = JsonConvert.DeserializeObject<Mobile[]>(usr.Mobile)
                }
                )
                .ToList()
                ;

            if (users.Count == 0)
            {
                return null;
            }

            var user = users[0];

            var groups =
                from gr in _context.Groups.Include(g => g.Organization)
                join usgr in _context.UserGroups.Include(ug => ug.User) on gr.Id equals usgr.GroupId
                where usgr.User.PersonalId.Equals(user.Id)
                select new { Group = gr, isMain = usgr.IsMain };

            var tt = groups.ToArray();

            user.Groups = groups.Select(gr => Mapper.Map(gr.Group)).ToArray();

            var mainGroup = groups.First(g => g.isMain).Group;
            user.MainGroup = Mapper.Map(mainGroup);

            var roles =
                from role in _context.Roles
                join usrl in _context.UserRoles.Include(ur => ur.User) on role.Id equals usrl.RoleId
                where usrl.User.PersonalId.Equals(user.Id)
                select new { Role = role, isMain = usrl.IsMain };

            user.Roles = roles.Select(role => Mapper.Map(role.Role)).ToArray();

            var mainRole = roles.First(r => r.isMain).Role;
            user.MainRole = Mapper.Map(mainRole);

            return user;
        }

        public UserDTO GetLightUser(string id)
        {
            var users = (
                from usr in _context.Users.Include(u => u.Organization)
                where usr.Id.Equals(id)
                select new UserDTO
                {
                    Id = usr.PersonalId,
                    FirstName = usr.FirstName,
                    LastName = usr.LastName,
                    Organization = Mapper.Map(usr.Organization),
                    Email = JsonConvert.DeserializeObject<Email[]>(usr.Email),
                    WorkPhone = JsonConvert.DeserializeObject<Phone[]>(usr.WorkPhone),
                    PrivatePhone = JsonConvert.DeserializeObject<Phone[]>(usr.PrivatePhone),
                    Mobile = JsonConvert.DeserializeObject<Mobile[]>(usr.Mobile)
                }
                )
                .ToList()
                ;

            if (users.Count == 0)
            {
                return null;
            }

            var user = users[0];

            return user;
        }

        public string Create(FrontendUserDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Id))
            {
                throw new ArgumentException("personal id missing");
            }

            var personalId = dto.Id;
            var id = Guid.NewGuid();

            if (_context.Users.FirstOrDefault(u => u.PersonalId.Equals(personalId)) != null)
            {
                throw new ArgumentException($"{personalId} already existed");
            }

            var orgid = dto.OrganizationId;

            //check org exist
            Organization org = _context.Organizations.FirstOrDefault(o => o.Id.Equals(orgid));
            if (org == null)
            {
                throw new ArgumentException($"organization {orgid} does not exist");
            }

            ////check all group exist
            //foreach (var groupDto in dto.Groups)
            //{
            //    if (_context.Groups.FirstOrDefault(gr => groupDto.Id.Equals(gr.Id.ToString())) == null)
            //    {
            //        throw new ArgumentException($"group {groupDto.Id} does not exist");
            //    }
            //}

            ////check all role exist
            //foreach (var roleDto in dto.Roles)
            //{
            //    if (_context.Roles.FirstOrDefault(rl => roleDto.Id.Equals(rl.Id.ToString())) == null)
            //    {
            //        throw new ArgumentException($"role {roleDto.Id} does not exist");
            //    }
            //}

            //make sure that one and only one group is main
            try
            {
                dto.Groups.Single(grDTO => grDTO.IsMain);
            }
            catch (InvalidOperationException ex)
            {
                throw new ArgumentException("one and only one main group must exist");
            }

            //make sure that one and only one role is main
            try
            {
                dto.Roles.Single(rlDTO => rlDTO.IsMain);
            }
            catch (InvalidOperationException ex)
            {
                throw new ArgumentException("one and only one main role must exist");
            }

            var userGroups =
                //(
                //    from gr in _context.Groups
                //    where dto.Groups.FirstOrDefault(gDto => gDto.Id.Equals(gr.Id.ToString())) != null
                //    select new UserGroup()
                //    {
                //        GroupId = gr.Id,
                //        UserId = id,
                //        IsMain = dto.MainGroup.Id.Equals(gr.Id.ToString())
                //    }
                //)
                //.ToArray()
                dto.Groups.Select(
                    grDTO => new UserGroup()
                    {
                        GroupId = grDTO.Id,
                        UserId = id,
                        IsMain = grDTO.IsMain
                    }
                    )
                    .ToArray()
                ;

            var userRoles =
                //(
                //    from rl in _context.Roles
                //    where dto.Roles.FirstOrDefault(rDto => rDto.Id.Equals(rl.Id.ToString())) != null
                //    select new UserRole()
                //    {
                //        RoleId = rl.Id,
                //        UserId = id,
                //        IsMain = dto.MainRole.Id.Equals(rl.Id.ToString())
                //    }
                //)
                //.ToArray()
                dto.Roles.Select(
                    rlDTO => new UserRole()
                    {
                        RoleId = rlDTO.Id,
                        UserId = id,
                        IsMain = rlDTO.IsMain
                    }
                    )
                    .ToArray()
                ;

            User user = new User()
            {
                Id = id,

                PersonalId = personalId,

                FirstName = dto.FirstName,
                LastName = dto.LastName,
                ProfileImage = dto.ProfileImage,

                OrganizationId = org.Id,

                Email = JsonConvert.SerializeObject(dto.Email),
                WorkPhone = JsonConvert.SerializeObject(dto.WorkPhone),
                PrivatePhone = JsonConvert.SerializeObject(dto.PrivatePhone),
                Mobile = JsonConvert.SerializeObject(dto.Mobile),
            };

            _context.UserGroups.AddRange(userGroups);
            _context.UserRoles.AddRange(userRoles);

            _context.Users.Add(user);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException dbe)
            {
                //todo log exception
                throw new ArgumentException(dbe.InnerException.Message);
            }

            return personalId;
        }

        public string Update(FrontendUserDTO dto)
        {
            var personalId = dto.Id;
            var oldUser = _context.Users.Include(usr => usr.Organization).FirstOrDefault(u => u.PersonalId.Equals(personalId));
            if (oldUser == null)
            {
                throw new KeyNotFoundException();
            }

            var orgid = dto.OrganizationId;

            //check org exist
            Organization org = _context.Organizations.FirstOrDefault(o => o.Id.Equals(orgid));
            if (org == null)
            {
                throw new ArgumentException($"organization {orgid} does not exist");
            }

            //make sure that one and only one group is main
            try
            {
                dto.Groups.Single(grDTO => grDTO.IsMain);
            }
            catch (InvalidOperationException ex)
            {
                throw new ArgumentException("one and only one main group must exist");
            }

            //make sure that one and only one role is main
            try
            {
                dto.Roles.Single(rlDTO => rlDTO.IsMain);
            }
            catch (InvalidOperationException ex)
            {
                throw new ArgumentException("one and only one main role must exist");
            }

            var userGroups =
                dto.Groups.Select(
                    grDTO => new UserGroup()
                    {
                        GroupId = grDTO.Id,
                        UserId = oldUser.Id,
                        IsMain = grDTO.IsMain
                    }
                    )
                    .ToArray()
                ;

            var userRoles =
                dto.Roles.Select(
                    rlDTO => new UserRole()
                    {
                        RoleId = rlDTO.Id,
                        UserId = oldUser.Id,
                        IsMain = rlDTO.IsMain
                    }
                    )
                    .ToArray()
                ;

            #region update user

            oldUser.FirstName = dto.FirstName;
            oldUser.LastName = dto.LastName;
            oldUser.ProfileImage = dto.ProfileImage;

            oldUser.OrganizationId = org.Id;

            oldUser.Email = JsonConvert.SerializeObject(dto.Email);
            oldUser.WorkPhone = JsonConvert.SerializeObject(dto.WorkPhone);
            oldUser.PrivatePhone = JsonConvert.SerializeObject(dto.PrivatePhone);
            oldUser.Mobile = JsonConvert.SerializeObject(dto.Mobile);

            oldUser.UserGroups = userGroups.ToList();
            oldUser.UserRoles = userRoles.ToList();

            #endregion

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

            return personalId;
        }

        public bool Delete(string id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(usr => usr.PersonalId.Equals(id));

                if (user == null)
                {
                    return false;
                }

                var userGroups = _context.UserGroups.Include(usgr => usgr.User).Where(usgr => usgr.User.PersonalId.Equals(id)).ToArray();
                _context.UserGroups.RemoveRange(userGroups);

                var userRoles = _context.UserRoles.Include(usrl => usrl.User).Where(usrl => usrl.User.PersonalId.Equals(id)).ToArray();
                _context.UserRoles.RemoveRange(userRoles);

                _context.Users.Remove(user);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //todo logging
                return false;
            }

            return true;
        }
    }
}