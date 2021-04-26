using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;
using Entities.RequestFeatures;


namespace Contracts
{
    public interface IOrganizationRepository
    {
        PagedList<Organization> GetAllOrganizations(OrganizationParameters organizationParameters ,bool trackChanges);
        Organization GetOrganization(Guid companyId, bool trackChanges);
        void CreateOrganization(Organization organization);

        void DeleteOrganization(Organization organization);


    }
}
