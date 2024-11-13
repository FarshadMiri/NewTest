using Microsoft.EntityFrameworkCore;
using TestWithValue.Domain.Enitities;
using System.Threading.Tasks;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Data;
using System.Linq.Expressions;
using System.Net.Sockets;

namespace TestWithValue.Persistence.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TestWithValueDbContext _context;

        public TicketRepository(TestWithValueDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTicketAsync(Tbl_Ticket ticket)
        {
            try
            {
                ticket.CreatedAt = DateTime.Now; // ثبت زمان ایجاد
                _context.tbl_Tickets.Add(ticket); // اضافه کردن تیکت به دیتابیس
                await _context.SaveChangesAsync(); // ذخیره تغییرات
                return true;    
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException.Message;
                inner=inner.Trim();
                return false;

                
            }
           
        }

        

        public async Task SaveMessageAsync(Tbl_TicketMessage ticketMessage)
        {
            try
            {
                _context.tbl_TicketMessages.Add(ticketMessage); // اضافه کردن پیام به جدول پیام‌ها
                await _context.SaveChangesAsync(); // ذخیره تغییرات

            }
            catch (Exception ex)
            {
                var inner = ex.InnerException.Message;
                inner=  inner.Trim();

                throw;
            }
        }

        public async Task<Tbl_Ticket> GetTicketByIdAsync(int ticketId)
        {
            return await _context.tbl_Tickets
                .Include(t => t.Messages) // شامل کردن پیام‌ها
                .Include(t => t.TicketStatus) // شامل کردن وضعیت تیکت
                .FirstOrDefaultAsync(t => t.Id == ticketId); // یافتن تیکت براساس ID
        }

        public async Task<bool> UserHasOpenTicketAsync(string userId)
        {
            return await _context.tbl_Tickets
            .AnyAsync(t => t.UserId == userId && t.TicketStatusId == (int)TicketStatus.Open);
        }

        public async Task<Tbl_Ticket> GetOpenTicketForUserByTitleAsync(string userId, string title)
        {
            return await _context.tbl_Tickets
                .Where(t => t.UserId == userId && t.Title == title && t.TicketStatusId == (int)TicketStatus.Open) // فقط تیکت‌های باز را بگرد
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Tbl_Ticket>> GetAllTicketsAsync()
        {
            return await _context.tbl_Tickets.ToListAsync();
        }

        public async Task<IEnumerable<Tbl_TicketMessage>> GetMessagesByTicketIdAsync(int ticketId)
        {
            return await _context.tbl_TicketMessages
          .Where(msg => msg.TicketId == ticketId)
          .OrderBy(msg => msg.SentAt)
          .ToListAsync();
        }

        public async Task<Tbl_Ticket> GetOpenTicketForUserAsync(string userId)
        {
            return await _context.tbl_Tickets
        .FirstOrDefaultAsync(t => t.UserId == userId && t.TicketStatusId == (int)TicketStatus.Open);
        }

       

        public void UpdateTicket(Tbl_Ticket ticket)
        {
            _context.tbl_Tickets.Update(ticket);
            _context.SaveChanges(); 

        }

        public async Task<IEnumerable<Tbl_Ticket>> GetTicketsByUserIdAsync(string userId)
        {
            return await _context.tbl_Tickets
                             .Where(t => t.UserId == userId)
                             .ToListAsync();
        }
    }
}
