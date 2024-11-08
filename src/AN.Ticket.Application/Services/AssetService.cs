using AN.Ticket.Application.DTOs.Asset;
using AN.Ticket.Application.Helpers.Pagination;
using AN.Ticket.Application.Interfaces;
using AN.Ticket.Application.Services.Base;
using AN.Ticket.Domain.Entities;
using AN.Ticket.Domain.EntityValidations;
using AN.Ticket.Domain.Interfaces;
using AN.Ticket.Domain.Interfaces.Base;

namespace AN.Ticket.Application.Services;
public class AssetService : Service<AssetDto, Asset>, IAssetService
{
    private readonly IAssetRepository _assetRepository;

    public AssetService(
        IRepository<Asset> repository,
        IAssetRepository assetRepository
    )
        : base(repository)
    {
        _assetRepository = assetRepository;
    }

    public async Task<PagedResult<AssetDto>> GetPaginatedAssetsAsync(int pageNumber, int pageSize, string searchTerm = "")
    {
        var (assets, totalItems) = await _assetRepository.GetPaginatedAssetsAsync(pageNumber, pageSize, searchTerm);

        var assetDTOs = assets.Select(a => new AssetDto
        {
            Id = a.Id,
            Name = a.Name,
            SerialNumber = a.SerialNumber,
            AssetType = a.AssetType,
            PurchaseDate = a.PurchaseDate,
            Value = a.Value,
            Description = a.Description,
            CreatedAt = a.CreatedAt,
            UpdatedAt = a.UpdatedAt
        }).ToList();

        return new PagedResult<AssetDto>
        {
            Items = assetDTOs,
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<bool> DeleteAssetsAsync(List<Guid> ids)
    {
        var assets = await _assetRepository.GetAllAsync();
        var assetsToDelete = assets.Where(a => ids.Contains(a.Id)).ToList();

        if (!assetsToDelete.Any())
            return false;

        foreach (var asset in assetsToDelete)
            _assetRepository.Delete(asset);

        return true;
    }

    public async Task<bool> DeleteAssetAsync(Guid id)
    {
        var asset = await _assetRepository.GetByIdAsync(id);

        if (asset is null)
            throw new EntityValidationException("Ativo não encontrado.");

        _assetRepository.Delete(asset);

        return true;
    }
}
