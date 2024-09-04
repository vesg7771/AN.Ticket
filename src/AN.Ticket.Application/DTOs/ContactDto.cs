namespace AN.Ticket.Application.DTOs;
public class ContactDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string PrimaryEmail { get; set; }
    public string SecondaryEmail { get; set; }
    public string Phone { get; set; }
    public string Mobile { get; set; }
    public string Department { get; set; }
    public string Title { get; set; }
    public List<SocialNetworkDto> SocialNetworks { get; set; }
    public Guid UserId { get; set; }
    public UserDto User { get; set; }
}

public class SocialNetworkDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public Guid ContactId { get; set; }
}