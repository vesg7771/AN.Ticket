using AN.Ticket.Application.Helpers.OpenAI;

namespace AN.Ticket.Application.Interfaces;
public interface IChatGptService
{
    Task<List<MessageResponse>> GenerateResponseAsync(string message);
}
