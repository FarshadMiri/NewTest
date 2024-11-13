using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.Enitities
{
	public class Tbl_City
	{
		[Key]
		public int CityId { get; set; }
		public string Name { get; set; }
		[ForeignKey("province")]
		public int ProvinceId { get; set; }
		public Tbl_Province province { get; set; }


	}
}
