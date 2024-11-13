using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.Contract.Persistence
{
    public interface ITicketRepository
    {
        Task<bool> CreateTicketAsync(Tbl_Ticket ticket); // برای ایجاد تیکت
        void UpdateTicket(Tbl_Ticket ticket);
        Task SaveMessageAsync(Tbl_TicketMessage ticketMessage); // برای ذخیره پیام
        Task<Tbl_Ticket> GetTicketByIdAsync(int ticketId); // برای دریافت تیکت براساس ID
        Task<bool> UserHasOpenTicketAsync(string userId);
        Task<Tbl_Ticket> GetOpenTicketForUserByTitleAsync(string userId,string title);
        Task<Tbl_Ticket> GetOpenTicketForUserAsync(string userId);
        Task<IEnumerable<Tbl_Ticket>> GetAllTicketsAsync();
        Task<IEnumerable<Tbl_TicketMessage>> GetMessagesByTicketIdAsync(int ticketId);
        Task<IEnumerable<Tbl_Ticket>> GetTicketsByUserIdAsync(string userId);
    }

}
