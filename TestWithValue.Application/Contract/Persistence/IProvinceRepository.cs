using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.Contract.Persistence
{
	public interface IProvinceRepository
	{
		Task<IEnumerable<Tbl_Province>> GetAllProvinces();
		Task<Tbl_Province> GetProvinceById(int id);

	}
}
