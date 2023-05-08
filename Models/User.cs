namespace MessengerApp.Models;
public class User
{
    public string Id{get; set;} = null!;
    public string UserName{get; set;} = null!;
    public string Email{get; set;} = null!;
    public string? Avatar{get; set;}
}