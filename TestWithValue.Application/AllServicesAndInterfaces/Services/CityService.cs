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
	public class CityService : ICityService
	{
		private readonly ICityRepository _cityRepository;
		public CityService(ICityRepository cityRepository)
		{
			_cityRepository = cityRepository;
		}
		public IEnumerable<Tbl_City> GetAllCities()
		{
			return _cityRepository.GetAllCity();
		}

		public async Task<Tbl_City> GetCityByCityIdAsync(int id)
		{
			return await _cityRepository.GetCityByCityIdAsync(id);
		}

		public IEnumerable<Tbl_City> GetCityByProvinceId(int provinceId)
		{
			return _cityRepository.GetCityByProvinceId(provinceId);
		}
	}
}
