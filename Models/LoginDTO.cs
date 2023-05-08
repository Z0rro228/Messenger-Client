using System.ComponentModel.DataAnnotations;
namespace MessengerApp.Models;
public class LoginDTO
{
    [Required]
    [StringLength(15, ErrorMessage = "Your Password is limited to {2} to {1} characters", MinimumLength = 6)]
    public string Password {get; set;} = null!;

    public string UserName{get; set;} = null!;
}

