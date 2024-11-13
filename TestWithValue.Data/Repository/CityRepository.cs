using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Data.Repository
{
	public class CityRepository : ICityRepository
	{
		private readonly TestWithValueDbContext _context;	
		public CityRepository(TestWithValueDbContext context)
		{
			_context = context;
		}
		public IEnumerable<Tbl_City> GetAllCity()
		{
			return _context.tbl_Cities.ToList();
		}

		public async Task<Tbl_City> GetCityByCityIdAsync(int id)
		{
			return await _context.tbl_Cities.FindAsync(id);
		}

		public IEnumerable<Tbl_City> GetCityByProvinceId(int provinceId)
		{
			return _context.tbl_Cities.Where(c => c.ProvinceId == provinceId).ToList();
		}

	}
}
