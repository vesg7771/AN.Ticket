using AN.Ticket.Application.DTOs.Attachment;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Application.Services.Base;
using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Domain.Interfaces.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AN.Ticket.Application.Services;
public class AttachmentService
    : Service<AttachmentDto, Attachment>, IAttachmentService
{
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AttachmentService(
        IRepository<Attachment> repository,
        IAttachmentRepository attachmentRepository,
        IUnitOfWork unitOfWork
    )
        : base(repository)
    {
        _attachmentRepository = attachmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> UploadAttachmentAsync(IFormFile file, Guid ticketId)
    {
        if (file == null || file.Length == 0)
            throw new FileNotFoundException("Nenhum arquivo foi enviado.");

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        var attachment = new Attachment(file.FileName, memoryStream.ToArray(), file.ContentType, ticketId);
        await _attachmentRepository.SaveAsync(attachment);
        await _unitOfWork.CommitAsync();

        return attachment.Id;
    }

    public async Task<FileResult> DownloadAttachmentAsync(Guid id)
    {
        var attachment = await _attachmentRepository.GetByIdAsync(id);

        if (attachment is null)
            throw new FileNotFoundException("Anexo não encontrado.");

        if (attachment.Content == null || attachment.Content.Length == 0)
            throw new InvalidOperationException("O conteúdo do anexo está vazio ou corrompido.");

        if (string.IsNullOrEmpty(attachment.ContentType))
            throw new InvalidOperationException("Tipo de conteúdo inválido.");

        return new FileContentResult(attachment.Content, attachment.ContentType)
        {
            FileDownloadName = attachment.FileName
        };
    }
}
