namespace AN.Ticket.WebUI.ViewModels.Team;

public class ProfileSettingsViewModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string ProfilePicture { get; set; }
    public List<TeamViewModel> Teams { get; set; }
}

public class TeamViewModel
{
    public string TeamName { get; set; }
    public List<TeamMemberViewModel> Members { get; set; }
}

public class TeamMemberViewModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string ProfilePicture { get; set; }
}
