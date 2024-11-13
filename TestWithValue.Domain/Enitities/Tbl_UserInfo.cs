using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.Enitities
{
	public class Tbl_UserInfo
	{
		[Key]
		public int UserInfoId { get; set; }

		[ForeignKey("User")]
		public string UserId { get; set; } // این کلید خارجی به جدول IdentityUser است

		public string FullName { get; set; }
		public string NationalCode { get; set; }
		public string Province { get; set; }
		public string City { get; set; }
		public string Organization { get; set; }

		// ارتباط با IdentityUser
		public virtual IdentityUser User { get; set; }
	}
}
