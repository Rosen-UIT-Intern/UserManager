using System;
using System.Collections.Generic;

using UserManager.Contract.DTOs;

namespace UserManager.Contract
{
    public interface IOrganizationService
    {
        IEnumerable<OrganizationDTO> GetOrganizations();
        OrganizationDTO GetOrganization(Guid id);
    }
}
