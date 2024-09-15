using AN.Ticket.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AN.Ticket.WebUI.Controllers;

[Authorize]
public class AttachmentController : Controller
{
    private readonly IAttachmentService _attachmentService;

    public AttachmentController(
        IAttachmentService attachmentService
    )
        => _attachmentService = attachmentService;

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(IFormFile file, Guid ticketId)
    {
        if (file == null || file.Length == 0)
        {
            TempData["ErrorMessage"] = "Por favor, selecione um arquivo para enviar.";
            return RedirectToAction("Details", "Ticket", new { id = ticketId });
        }

        if (file.Length > 10485760)
        {
            TempData["ErrorMessage"] = "O arquivo excede o tamanho máximo permitido de 10 MB.";
            return RedirectToAction("Details", "Ticket", new { id = ticketId });
        }

        var allowedTypes = new List<string> { "application/pdf", "image/jpeg", "image/png" };
        if (!allowedTypes.Contains(file.ContentType))
        {
            TempData["ErrorMessage"] = "Tipo de arquivo não suportado.";
            return RedirectToAction("Details", "Ticket", new { id = ticketId });
        }

        try
        {
            var attachmentId = await _attachmentService.UploadAttachmentAsync(file, ticketId);
            TempData["SuccessMessage"] = "Arquivo enviado com sucesso.";
            TempData["AttachmentId"] = attachmentId;
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Erro ao enviar o arquivo: {ex.Message}";
        }

        return RedirectToAction("Details", "Ticket", new { id = ticketId });
    }

    [HttpGet]
    public async Task<IActionResult> Download(Guid id, Guid ticketId)
    {
        try
        {
            var fileResult = await _attachmentService.DownloadAttachmentAsync(id);
            return fileResult;
        }
        catch (FileNotFoundException)
        {
            TempData["ErrorMessage"] = "Arquivo não encontrado.";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Erro ao baixar o arquivo: {ex.Message}";
        }

        return RedirectToAction("Details", "Ticket", new { id = ticketId });
    }
}
