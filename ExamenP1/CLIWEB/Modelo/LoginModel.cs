using System.ComponentModel.DataAnnotations;

namespace CLIWEB.Modelo;

public class LoginModel
{
    [Required(ErrorMessage = "El usuario es requerido.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "La clave es requerida.")]
    public string Password { get; set; } = string.Empty;
}
