using System.Net.Http.Json;
using System.Net;
namespace MauiApp3;

public class Program
{

    static HttpClient httpClient = new HttpClient();

    static string address = "https://localhost:7293/";
    static async Task Register(RegisterModel model)
    {
        JsonContent content = JsonContent.Create(model);
        using var response = await httpClient.PostAsync(address + "api/auth/register", content);
        Console.WriteLine(response.StatusCode);

    }
    public static async Task Login(LoginModel model)
    {
        using var response = await httpClient.PostAsJsonAsync(address + "api/auth/login", model);
        Console.WriteLine(response.StatusCode);
    }
    static async Task Logout()
    {
        using var response = await httpClient.GetAsync(address + "api/auth/logout");
        Console.WriteLine(response.StatusCode);
    }
    static async Task GetTestEndPoint()
    {
        using var response = await httpClient.GetAsync(address + "api/auth/test");
        Console.WriteLine(response.StatusCode);
    }
}

public class LoginModel
{
    public string Password { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public LoginModel(string username, string password)
    {
        UserName = username;
        Password = password;
    }
}
public class RegisterModel
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public RegisterModel(string email, string password, string username)
    {
        Email = email;
        Password = password;
        UserName = username;
    }
}
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
    async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistrationPage());
    }
    void OnShowRegisterPasswordButtonClicked(object sender, EventArgs e)
    {
        if (RegistrationPassword.IsPassword)
        {
            ShowRegisterPasswordButton.Source = "eye_closed.png";
            RegistrationPassword.IsPassword = false;
        }
        else
        {
            ShowRegisterPasswordButton.Source = "eye_open.png";
            RegistrationPassword.IsPassword = true;
        }
    }
    async void OnLogInButtonClicked(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new ChatsPage());
    }
}
