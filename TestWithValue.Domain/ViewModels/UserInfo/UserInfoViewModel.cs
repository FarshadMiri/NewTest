using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Domain.ViewModels.UserInfo
{
	public class UserInfoViewModel
	{
		public string UserId { get; set; } // این کلید خارجی به جدول IdentityUser است
		public string FullName { get; set; }
		public string NationalCode { get; set; }
		public int ProvinceId { get; set; }
		public int CityId { get; set; }
		public string? Province { get; set; } // برای ذخیره نام استان
		public string? City{ get; set; } // برای ذخیره نام شهر
		public IEnumerable<Tbl_Province> Provinces { get; set; }
		public IEnumerable<Tbl_City> Cities { get; set; }

		public string Organization { get; set; }
		public IEnumerable<Tbl_Organization>  Organizations { get; set; }



	}
}
