using AN.Ticket.Application.DTOs.User;

namespace AN.Ticket.Application.DTOs.Contact;
public class ContactCreateDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Cpf { get; set; }
    public string PrimaryEmail { get; set; }
    public string SecondaryEmail { get; set; }
    public string Phone { get; set; }
    public string Mobile { get; set; }
    public string Department { get; set; }
    public string Title { get; set; }
    public List<SocialNetworkDto> SocialNetworks { get; set; }
    public Guid UserId { get; set; }
    public UserDto User { get; set; }
    public Guid PaymentPlanId { get; set; }
}
