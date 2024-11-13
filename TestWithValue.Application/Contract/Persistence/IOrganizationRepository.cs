using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.Contract.Persistence
{
	public interface IOrganizationRepository
	{
		Task<IEnumerable<Tbl_Organization>> GetAllOrganization();
        Task<Tbl_Organization> GetOrganizationByIdAsync(int id);

    }
}
