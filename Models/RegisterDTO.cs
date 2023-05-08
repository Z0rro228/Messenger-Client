using System.ComponentModel.DataAnnotations;
namespace MessengerApp.Models;
public class RegisterDTO :LoginDTO
{   [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
}