using Microsoft.AspNetCore.Http;

namespace AN.Ticket.Application.Interfaces;
public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile file);
    Task DeleteFileAsync(string filePath);
}
