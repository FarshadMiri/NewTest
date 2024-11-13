using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.Enitities
{
	public class Tbl_Province
	{
		[Key]
		public int ProvinceId { get; set; }
		public string Name { get; set; }
		public IEnumerable<Tbl_City>  cities { get; set; }
	}
}
