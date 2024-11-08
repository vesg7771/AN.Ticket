using System.ComponentModel.DataAnnotations;

namespace AN.Ticket.Application.DTOs.Asset;
public class AssetDto
{
    public Guid? Id { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "O nome do ativo deve ter no máximo 100 caracteres.")]
    [Display(Name = "Nome")]
    public string Name { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "O número de série deve ter no máximo 100 caracteres.")]
    [Display(Name = "Número de Série")]
    public string SerialNumber { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "O tipo do ativo deve ter no máximo 50 caracteres.")]
    [Display(Name = "Tipo de Ativo")]
    public string AssetType { get; set; }

    [Required]
    [Display(Name = "Data de Compra")]
    [DataType(DataType.Date)]
    public DateTime PurchaseDate { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    [Display(Name = "Valor")]
    public decimal Value { get; set; }

    [Display(Name = "Descrição")]
    [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
    public string? Description { get; set; }

    public Guid? DepartmentId { get; set; }

    public string? Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
