using AN.Ticket.Application.DTOs.User;
using System.ComponentModel.DataAnnotations;

namespace AN.Ticket.Application.DTOs.Contact;
public class ContactCreateDto
{
    [Required(ErrorMessage = "Esse campo é obrigatório.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Esse campo é obrigatório.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Esse campo é obrigatório.")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "Esse campo é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    public string PrimaryEmail { get; set; }

    [EmailAddress(ErrorMessage = "Email inválido.")]
    public string SecondaryEmail { get; set; }

    [Phone(ErrorMessage = "Telefone inválido.")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Esse campo é obrigatório.")]
    [Phone(ErrorMessage = "Telefone inválido.")]
    public string Mobile { get; set; }

    public string Department { get; set; }

    public string Title { get; set; }

    public List<SocialNetworkDto> SocialNetworks { get; set; }

    public Guid UserId { get; set; }

    public UserDto User { get; set; }

    public Guid PaymentPlanId { get; set; }
}
