using AN.Ticket.Application.DTOs.SatisfactionRating;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Domain.EntityValidations;
using Microsoft.AspNetCore.Mvc;

namespace AN.Ticket.WebUI.Controllers;
public class SatisfactionRatingController : Controller
{
    private readonly ISatisfactionRatingService _satisfactionRatingService;

    public SatisfactionRatingController(
        ISatisfactionRatingService satisfactionRatingService
    )
    {
        _satisfactionRatingService = satisfactionRatingService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(Guid ticketId)
    {
        var ratingDto = await _satisfactionRatingService.GetRatingByTicketIdAsync(ticketId);
        ViewBag.TicketId = ticketId;

        return View(ratingDto ?? new SatisfactionRatingDto { TicketId = ticketId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(SatisfactionRatingDto ratingDto)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Por favor, preencha todos os campos obrigatórios.";
            return View(nameof(Index), ratingDto);
        }

        try
        {
            await _satisfactionRatingService.SaveOrUpdateRatingAsync(ratingDto);
            TempData["SuccessMessage"] = "Avaliação salva com sucesso!";
            return RedirectToAction(nameof(ThankYou));
        }
        catch (EntityValidationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return View(nameof(Index), ratingDto);
        }
    }

    [HttpGet]
    public IActionResult ThankYou()
    {
        return View();
    }
}
