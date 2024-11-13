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
	public class ProvinceRepository : IProvinceRepository
	{
		private readonly TestWithValueDbContext _context;
		public ProvinceRepository(TestWithValueDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<Tbl_Province>> GetAllProvinces()
		{
			return await _context.tbl_Provinces.ToListAsync();
		}

		public async Task<Tbl_Province> GetProvinceById(int id)
		{
			return await _context.tbl_Provinces.FindAsync(id);
		}
	}
}
