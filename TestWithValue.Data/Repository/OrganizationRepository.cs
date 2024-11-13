using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Data.Repository
{
	public class OrganizationRepository : IOrganizationRepository
	{
		private readonly TestWithValueDbContext _context;
        public OrganizationRepository(TestWithValueDbContext context)
        {
				_context = context;
		}

		public async Task<IEnumerable<Tbl_Organization>> GetAllOrganization()
		{
			return await _context.tbl_Organizations.ToListAsync();
		}
        public async Task<Tbl_Organization> GetOrganizationByIdAsync(int id)
        {
            return await _context.tbl_Organizations.FindAsync(id);
        }
    }
}
