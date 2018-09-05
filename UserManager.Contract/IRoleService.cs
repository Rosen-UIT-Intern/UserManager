using System;
using System.Collections.Generic;

using UserManager.Contract.DTOs;

namespace UserManager.Contract
{
    public interface IRoleService
    {
        IEnumerable<RoleDTO> GetRoles();
        RoleDTO GetRole(Guid id);
        IEnumerable<UserDTO> GetUsers(Guid roleId);
    }
}
