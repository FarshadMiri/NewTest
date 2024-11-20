using AutoMapper;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;
using TestWithValue.Domain.ViewModels.Ticket;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;

    public TicketService(ITicketRepository ticketRepository, IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
    }

    public async Task CreateTicketAsync(TicketViewModel model)
    {
        var ticket = new Tbl_Ticket
        {
            Title = model.Title,
            Description = model.Description,
            UserId = model.UserId,
            TicketStatusId = (int)TicketStatus.Open // وضعیت اولیه تیکت
        };

        var tickets = await _ticketRepository.CreateTicketAsync(ticket);

    }

    public async Task SaveMessageAsync(int ticketId, string senderId, string message)
    {
        var ticketMessage = new Tbl_TicketMessage
        {
            TicketId = ticketId,
            SenderId = senderId,
            Message = message,
            SentAt = DateTime.Now
        };

        await _ticketRepository.SaveMessageAsync(ticketMessage); // ذخیره پیام
    }

    public async Task<TicketDetailsViewModel> GetTicketDetailsAsync(int ticketId)
    {
        var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId); // دریافت اطلاعات تیکت
        if (ticket == null) return null;

        var viewModel = new TicketDetailsViewModel
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            UserId = ticket.UserId,
            TicketStatus = (TicketStatus)ticket.TicketStatusId,
            Messages = ticket.Messages.Select(m => new TicketMessageViewModel
            {
                SenderId = m.SenderId,
                Message = m.Message,
                SentAt = m.SentAt
            }).ToList()
        };

        return viewModel; // بازگشت ویو مدل جزئیات تیکت
    }

    public async Task<bool> UserHasOpenTicketAsync(string userId)
    {
        return await _ticketRepository.UserHasOpenTicketAsync(userId);
    }

    public async Task<TicketViewModel> GetOpenTicketForUserByTitleAsync(string userId, string title)
    {
        var ticket = await _ticketRepository.GetOpenTicketForUserByTitleAsync(userId, title);
        var ticketVM = _mapper.Map<TicketViewModel>(ticket);
        return ticketVM;
    }

    public async Task<IEnumerable<TicketViewModel>> GetAllTicketsAsync()
    {
        var tickets = await _ticketRepository.GetAllTicketsAsync();
        return _mapper.Map<List<TicketViewModel>>(tickets);
    }

    public async Task<IEnumerable<TicketMessageViewModel>> GetMessagesByTicketIdAsync(int ticketId)
    {
        var messages = await _ticketRepository.GetMessagesByTicketIdAsync(ticketId);
        return messages.Select(msg => new TicketMessageViewModel
        {
            SenderId = msg.SenderId,
            Message = msg.Message,
            SentAt = msg.SentAt
        });
    }

    public async Task<TicketViewModel> GetOpenTicketForUserAsync(string userId)
    {
        var ticket = await _ticketRepository.GetOpenTicketForUserAsync(userId);
        var ticketVM = _mapper.Map<TicketViewModel>(ticket);
        return ticketVM;
    }

    public async Task<bool> CloseTicketAsync(int ticketId)
    {
        var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
        if (ticket == null)
            return false;

        // بررسی وضعیت تیکت اگر بسته است
        if (ticket.TicketStatusId == (int)TicketStatus.Closed)
            return false;

        // تغییر وضعیت تیکت به بسته
        ticket.TicketStatusId = (int)TicketStatus.Closed;
        _ticketRepository.UpdateTicket(ticket);


        return true;
    }


    public async Task<TicketViewModel> GetTicketByIdAsync(int ticketId)
    {
        var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
        var ticketVM = _mapper.Map<TicketViewModel>(ticket);
        return ticketVM;

    }

    public async Task<IEnumerable<TicketViewModel>> GetAllTicketsByUserIdAsync(string userId)
    {
        var tickets = await _ticketRepository.GetTicketsByUserIdAsync(userId);

        // تبدیل Tbl_Ticket به TicketViewModel
        var ticketViewModels = tickets.Select(ticket => new TicketViewModel
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            UserId = ticket.UserId,
            IsClosed = ticket.TicketStatusId == (int)TicketStatus.Closed // یا بررسی وضعیت بستن تیکت
        }).ToList();

        return ticketViewModels;
    }

    public async Task UpdateMessageAsync(int ticketId, string newMessage)
    {
        // دریافت تمام پیام‌ها بر اساس TicketId
        var messages = await _ticketRepository.GetMessagesByTicketIdAsync(ticketId);

        if (messages == null || !messages.Any())
        {
            throw new Exception($"No messages found for Ticket ID {ticketId}");
        }

        // فرض: فقط اولین پیام را به‌روزرسانی می‌کنیم (یا معیار خاصی را انتخاب کنید)
        var ticketMessage = messages.First();
        ticketMessage.Message = newMessage;
        ticketMessage.SentAt = DateTime.UtcNow;

        // به‌روزرسانی پیام
        await _ticketRepository.UpdateMessageAsync(ticketMessage);
    }
}

