using System.ComponentModel.DataAnnotations;

namespace AN.Ticket.WebUI.ViewModels.Account;

public class SecuritySettingViewModel
{
    [Display(Name = "Senha Atual")]
    [Required(ErrorMessage = "A senha atual é obrigatória.")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }

    [Display(Name = "Nova Senha")]
    [Required(ErrorMessage = "A nova senha é obrigatória.")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Display(Name = "Confirmar Nova Senha")]
    [Required(ErrorMessage = "A confirmação da nova senha é obrigatória.")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "A nova senha e a confirmação da nova senha não coincidem.")]
    public string ConfirmNewPassword { get; set; }
}
