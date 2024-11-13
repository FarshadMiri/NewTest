using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services
{
	public class OrganizationService : IOrganizationService
	{
		private readonly IOrganizationRepository _organizationRepository;
        public OrganizationService(IOrganizationRepository organizationRepository)
        {
			_organizationRepository = organizationRepository;	
        }
        public async Task<IEnumerable<Tbl_Organization>> GetAllOrganizations()
		{
			return await _organizationRepository.GetAllOrganization();	
		}

        public async Task<Tbl_Organization> GetOrganizationByIdAsync(int id)
        {
            return await _organizationRepository.GetOrganizationByIdAsync(id);
        }
    }
}
