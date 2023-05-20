namespace MessengerApp.Services;
public class InternetProvider: IInternetProvider //TODO: ASK FOR DISPOSABLE
{
    private string _serverRootUrl = "http://localhost:5296";
    private HttpClient? _httpClient;
    private IChatService? _chatService;
    private IAuthService? _authService;
    private IMessagesService? _messagesService;
    private IUserService? _userService;
    private IChatHubService? _hubService;
    public IChatService ChatService{
        get
        {
            if(_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
            if(_chatService == null)
            {
                _chatService = new ChatService(_httpClient, _serverRootUrl);
            }
            return _chatService;
        }
    }
    public IAuthService AuthService{
        get
        {
            if(_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
            if(_authService == null)
            {
                _authService = new AuthService(_httpClient, _serverRootUrl);
            }
            return _authService;
        }
    }
    public IMessagesService MessagesService
    {
        get
        {
            if(_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
            if(_messagesService == null)
            {
                _messagesService = new MessagesService(_httpClient, _serverRootUrl);
            }
            return _messagesService;  
        }
    }
    public IUserService UserService
    {
        get
        {
            if(_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
            if(_userService == null)
            {
                _userService = new UserService(_httpClient, _serverRootUrl);
            }
            return _userService;
        }
    }
    public IChatHubService ChatHubService
    {
        get
        {
            if(_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
            if(_hubService == null)
            {
                _hubService = new ChatHubService(_httpClient, _serverRootUrl);
            }
            return _hubService;
        }
    }
}