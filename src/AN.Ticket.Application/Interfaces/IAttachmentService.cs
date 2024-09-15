using AN.Ticket.Application.DTOs.Attachment;
using AN.Ticket.Application.Interfaces.Base;
using AN.Ticket.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AN.Ticket.Application.Interfaces;

public interface IAttachmentService
    : IService<AttachmentDto, Attachment>
{
    Task<FileResult> DownloadAttachmentAsync(Guid id);
    Task<Guid> UploadAttachmentAsync(IFormFile file, Guid ticketId);
}
