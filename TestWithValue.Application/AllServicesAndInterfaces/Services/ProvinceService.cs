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
	public class ProvinceService : IProvinceService
	{
		private readonly IProvinceRepository _provinceRepository;
		public ProvinceService(IProvinceRepository provinceRepository)
		{
			_provinceRepository = provinceRepository;
		}


		public async Task<IEnumerable<Tbl_Province>> GetAllProvinces()
		{
			return await _provinceRepository.GetAllProvinces();
		}

		public async Task<Tbl_Province> GetProvinceById(int id)
		{
			return await _provinceRepository.GetProvinceById(id);
		}
	}
}
