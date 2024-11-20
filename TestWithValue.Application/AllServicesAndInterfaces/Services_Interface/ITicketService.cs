using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Ticket;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface
{
    public interface ITicketService
    {
        Task CreateTicketAsync(TicketViewModel model); // ایجاد تیکت
        Task SaveMessageAsync(int ticketId, string senderId, string message); // ذخیره پیام
        Task<bool> UserHasOpenTicketAsync(string userId);
        Task<TicketViewModel> GetOpenTicketForUserByTitleAsync(string userId, string title);
        Task<TicketViewModel> GetOpenTicketForUserAsync(string userId);
        Task<IEnumerable<TicketViewModel>> GetAllTicketsAsync();
        Task<IEnumerable<TicketMessageViewModel>> GetMessagesByTicketIdAsync(int ticketId);
        Task<TicketViewModel> GetTicketByIdAsync(int ticketId);
        Task<bool> CloseTicketAsync(int ticketId);
        Task<IEnumerable<TicketViewModel>> GetAllTicketsByUserIdAsync(string userId);
        Task UpdateMessageAsync(int ticketId, string newMessage);
    }
}
