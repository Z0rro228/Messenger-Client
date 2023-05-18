namespace MessengerApp.Services;
interface IServiceProvider
{
    IAuthService AuthService{get;}
    IChatService ChatService{get;}
    IMessagesService MessagesService{get;}
    IUserService UserService{get;}
    IChatHubService ChatHubService{get;}
}