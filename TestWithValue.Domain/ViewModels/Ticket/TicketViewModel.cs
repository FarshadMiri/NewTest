using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Domain.ViewModels.Ticket
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public bool IsClosed { get; set; }
    }
}
