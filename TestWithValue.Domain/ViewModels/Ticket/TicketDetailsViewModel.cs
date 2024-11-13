using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Domain.ViewModels.Ticket
{
    public class TicketDetailsViewModel
    {
        public int Id { get; set; } // شناسه تیکت
        public string Title { get; set; } // عنوان تیکت
        public string Description { get; set; } // توضیحات تیکت
        public string UserId { get; set; } // شناسه کاربر
        public TicketStatus TicketStatus { get; set; } // وضعیت تیکت
        public List<TicketMessageViewModel> Messages { get; set; } // لیست پیام‌ها
    }
}
