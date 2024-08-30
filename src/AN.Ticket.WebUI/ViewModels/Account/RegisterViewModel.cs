using System.ComponentModel.DataAnnotations;

namespace AN.Ticket.WebUI.ViewModels.Account;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Nome é obrigatório.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email não é um endereço de email válido.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email não é um endereço de email válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatório.")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "A senha deve conter no mínimo 8 caracteres.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$", ErrorMessage = "A senha deve ter letra maiúscula, minúscula, número e caractere especial.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Comfirme a senha")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirme a senha")]
    [Compare("Password", ErrorMessage = "As senhas não são iguais.")]
    public string ConfirmPassword { get; set; }

    //public string ReturnUrl { get; set; }

    public bool RememberMe { get; set; }
}