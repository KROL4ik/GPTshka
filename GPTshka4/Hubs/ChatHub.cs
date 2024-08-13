using apiTest.Models;
using GPTshka4.Context;
using GPTshka4.Models.YandexGPTModels;
using GPTshka4.Source;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Message = GPTshka4.Models.DbModels.Message;


namespace GPTshka4.Hubs
{
    public interface IChatClient
    {
        public Task ReceiveMessage(string userName,string message,DateTime dateTime);
    }
    public class ChatHub :Hub<IChatClient>
    {
       private readonly YandexGPTService _yandexGPTService;
       private readonly IHttpContextAccessor _httpContextAccessor;
       private readonly ApplicationContext _applicationContext;
        private readonly IHttpClientFactory _httpClientFactory;
       private readonly IMemoryCache _cache ;
        private readonly YandexGPTSettings _yandexGPTSettings;
       public ChatHub(IMemoryCache cache, IHttpClientFactory httpClientFactory, YandexGPTSettings yandexGPTSettings, IHttpContextAccessor httpContextAccessor, ApplicationContext applicationContext)
        {
            _yandexGPTService = new YandexGPTService(httpClientFactory, yandexGPTSettings);
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _applicationContext = applicationContext;
            _httpClientFactory = httpClientFactory;
            _yandexGPTSettings = yandexGPTSettings;
        }

        public async Task JoinChat(string userName)
        {
           
            await Groups.AddToGroupAsync(Context.ConnectionId, userName);

            _cache.Set(Context.ConnectionId, userName);
            
            await Clients
                .Group(userName)
                .ReceiveMessage("System", $"{userName} присоединился к чату",DateTime.Now);
        }

        public async Task Send(string message)
        {

                Answer response = new Answer();
                while (response.result == null)
                {
                    response = await _yandexGPTService.SendRequest(message, CompletionOptions.Create(0.6f, 2000));
                }
                var stringResponse = response.result.alternatives[0].message.text;
                Console.WriteLine("Aboba");


                await SaveMessage(message, true);
                await SaveMessage(stringResponse, false);
                var userName = _cache.Get(Context.ConnectionId).ToString();
                if (userName != null)
                {
                    await Clients
                      .Group(userName)
                      .ReceiveMessage(userName, stringResponse,DateTime.Now);
                }

            
            
           

        }
    
        public async Task SaveMessage(string message,bool isUser)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Message messageModel = new Message() { Date=DateTime.Now,Text= message, UserId=userId,IsUser=isUser};
            _applicationContext.Messages.Add(messageModel);
            await _applicationContext.SaveChangesAsync();
        }

    }
}
