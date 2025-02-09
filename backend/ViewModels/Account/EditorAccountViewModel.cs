using System.ComponentModel.DataAnnotations;
using backend.Attributes;
namespace backend.ViewModels.Account;

public class EditorAccountViewModel
{
    [Required(ErrorMessage = "Nome obrigátorio!")]
    [MinLength(5, ErrorMessage = "Nome deve ter no minímo 5 caracteres!")]
    [MaxLength(255, ErrorMessage = "Nome não deve passar de 255 caracteres!")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "E-mail obrigatório!")]
    [EmailAddress(ErrorMessage = "O e-mail deve ser válido!")]
    [MaxLength(255, ErrorMessage = "Email não deve passar de 255 caracteres!")]
    public string Email { get; set; } = null!;

    [StringNotNull(ErrorMessage = "Slug não pode ser nulo!")]
    public string? Slug { get; set; }
}