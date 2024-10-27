using AN.Ticket.Application.DTOs.Activity;

namespace AN.Ticket.WebUI.ViewModels.Activity;

public class ActivityListViewModel
{
    public IEnumerable<ActivityDto> Activities { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public string SearchTerm { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
}
