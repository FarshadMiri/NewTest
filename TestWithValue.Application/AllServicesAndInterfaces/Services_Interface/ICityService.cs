using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface
{
	public interface ICityService
	{
		IEnumerable<Tbl_City> GetAllCities();
		IEnumerable<Tbl_City> GetCityByProvinceId(int provinceId);
		Task<Tbl_City> GetCityByCityIdAsync(int id);
	}
}
