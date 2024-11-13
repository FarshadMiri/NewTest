using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.Contract.Persistence
{
	public interface ICityRepository
	{
		IEnumerable<Tbl_City> GetAllCity();

		IEnumerable<Tbl_City> GetCityByProvinceId(int provinceid);
		Task<Tbl_City> GetCityByCityIdAsync(int id);
	}
}
