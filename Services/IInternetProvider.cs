namespace MessengerApp.Services;
public interface IInternetProvider
{
    IAuthService AuthService{get;}
    IChatService ChatService{get;}
    IMessagesService MessagesService{get;}
    IUserService UserService{get;}
    IChatHubService ChatHubService{get;}
}