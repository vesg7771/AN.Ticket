using AN.Ticket.Application.DTOs.Home;

namespace AN.Ticket.Application.Interfaces;
public interface IHomeService
{
    Task<HomeDto> GetHomeData(Guid userId, DateTime startOfWeek, DateTime endOfWeek, bool showInProgress);
}
