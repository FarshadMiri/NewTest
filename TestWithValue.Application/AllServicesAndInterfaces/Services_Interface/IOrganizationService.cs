using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface
{
	public interface IOrganizationService
	{
		Task<IEnumerable<Tbl_Organization>> GetAllOrganizations();
		Task<Tbl_Organization> GetOrganizationByIdAsync(int id);

    }
}
