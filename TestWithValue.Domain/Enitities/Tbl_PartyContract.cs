using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.Enitities
{
    public class Tbl_PartyContract
    {
        [Key]
        public int ContractId { get; set; }          // شناسه منحصر به فرد قرارداد
        public int TitleId { get; set; }         // شناسه عنوان قرارداد
        public string TitleName {  get; set; }
        public string PartyOneId { get; set; }      // شناسه طرف اول قرارداد (کاربر)
        public string PartyTwoId { get; set; }      // شناسه طرف دوم قرارداد (کاربر یا وکیل)
        public string PartyOneName { get; set; }    // نام طرف اول قرارداد
        public string PartyTwoName { get; set; }    // نام طرف دوم قرارداد
        public DateTime ContractDate { get; set; }  // تاریخ ایجاد قرارداد
        public string Status { get; set; }          // وضعیت کلی قرارداد
        public string PartyOneStatus { get; set; }  // وضعیت تایید/رد طرف اول قرارداد
        public string PartyTwoStatus { get; set; }  // وضعیت تایید/رد طرف دوم قرارداد
        public string CreatedBy { get; set; }       // شناسه پشتیبان که قرارداد را ایجاد کرده
        public DateTime CreatedDate { get; set; }   // تاریخ ایجاد قرارداد

        // ارتباط با جدول Users (AspNetUsers):
        public virtual IdentityUser PartyOne { get; set; }  // ارجاع به طرف اول قرارداد (کاربر)
        public virtual IdentityUser PartyTwo { get; set; }  // ارجاع به طرف دوم قرارداد (کاربر یا وکیل)

        // ارجاع به عنوان قرارداد:
        public virtual Tbl_ContractTitle ContractTitle { get; set; }

        // ارتباط با جدول واسط:
        public virtual ICollection<Tbl_ContractClauseMapping> ContractClauseMappings { get; set; }
    }
}
