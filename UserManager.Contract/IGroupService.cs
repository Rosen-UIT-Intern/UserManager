using System;
using System.Collections.Generic;

using UserManager.Contract.DTOs;

namespace UserManager.Contract
{
    public interface IGroupService
    {
        IEnumerable<GroupDTO> GetAllGroup();
        IEnumerable<GroupDTO> GetAllGroupBelongToOrganization(Guid organizationId);
        GroupDTO GetGroup(Guid groupId);
        IEnumerable<UserDTO> GetUsers(Guid groupId);
    }
}
