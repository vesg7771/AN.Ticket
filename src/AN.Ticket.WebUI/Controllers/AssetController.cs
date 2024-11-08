using AN.Ticket.Application.Interfaces;
using AN.Ticket.WebUI.ViewModels.Asset;
using Microsoft.AspNetCore.Mvc;

namespace AN.Ticket.WebUI.Controllers;
public class AssetController : Controller
{
    private readonly IAssetService _assetService;

    public AssetController(
        IAssetService assetService
    )
        => _assetService = assetService;

    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, string searchTerm = "")
    {
        var paginatedAssets = await _assetService.GetPaginatedAssetsAsync(pageNumber, pageSize, searchTerm);

        return View(new AssetListViewModel
        {
            Assets = paginatedAssets.Items,
            PageNumber = paginatedAssets.PageNumber,
            PageSize = paginatedAssets.PageSize,
            TotalItems = paginatedAssets.TotalItems,
            SearchTerm = searchTerm
        });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _assetService.DeleteAssetAsync(id);
            TempData["SuccessMessage"] = "Ativo excluído com sucesso.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Erro ao excluir o ativo: {ex.Message}";
        }

        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAssets(List<Guid> ids)
    {
        if (ids == null || !ids.Any())
        {
            TempData["ErrorMessage"] = "Nenhum ativo foi selecionado.";
            return RedirectToAction(nameof(Index));
        }

        try
        {
            await _assetService.DeleteAssetsAsync(ids);
            TempData["SuccessMessage"] = "Ativos deletados com sucesso!";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Erro ao deletar os ativos";
        }

        return RedirectToAction(nameof(Index));
    }
}
