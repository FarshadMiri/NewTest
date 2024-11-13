using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface
{
	public interface IProvinceService
	{
		Task<IEnumerable<Tbl_Province>> GetAllProvinces();
		Task<Tbl_Province> GetProvinceById(int id);

	}
}
