namespace AN.Ticket.Domain.Accounts;

public interface ISeedUserRoleInitial
{
    Task SeedUsersAsync();
    Task SeedRolesAsync();
}
