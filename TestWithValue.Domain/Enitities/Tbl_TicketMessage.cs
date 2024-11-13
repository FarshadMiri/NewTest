using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithValue.Domain.Enitities
{
    public class Tbl_TicketMessage
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string SenderId { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
        public Tbl_Ticket Ticket { get; set; }
        public string? FilePath { get; set; } // مسیر فایل PDF گزارش
    }
}