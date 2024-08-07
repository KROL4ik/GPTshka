using apiTest.Models;
using GPTshka4.Context;
using GPTshka4.Models.YandexGPTModels;
using GPTshka4.Source;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Security.Claims;
using Message = GPTshka4.Models.DbModels.Message;


namespace GPTshka4.Hubs
{
    public interface IChatClient
    {
        public Task ReceiveMessage(string userName,string message);
    }
    public class ChatHub :Hub<IChatClient>
    {
       private readonly YandexGPTService _yandexGPTService;
       private readonly IHttpContextAccessor _httpContextAccessor;
       private readonly ApplicationContext _applicationContext;
       private readonly IMemoryCache _cache ;
       public ChatHub(IMemoryCache cache, IHttpClientFactory httpClientFactory, YandexGPTSettings yandexGPTSettings, IHttpContextAccessor httpContextAccessor, ApplicationContext applicationContext)
        {
            _yandexGPTService = new YandexGPTService(httpClientFactory, yandexGPTSettings);
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _applicationContext = applicationContext;
        }

        public async Task JoinChat(string userName)
        {
           
            await Groups.AddToGroupAsync(Context.ConnectionId, userName);

            _cache.Set(Context.ConnectionId, userName);
            
            await Clients
                .Group(userName)
                .ReceiveMessage("System", $"{userName} присоединился к чату");
        }

        public async Task Send(string message)
        {
            var response =  await _yandexGPTService.SendRequest(message,CompletionOptions.Create(0.6f,2000));
            var stringResponse = response.result.alternatives[0].message.text;
            Console.WriteLine("Aboba");
            Console.WriteLine(JsonConvert.SerializeObject(response));
            await SaveMessage(message);
            await SaveMessage(stringResponse);
            var userName = _cache.Get(Context.ConnectionId).ToString();
            if (userName != null)
            {
                //await Clients
                //    .Group(userName)
                //    .ReceiveMessage(userName, message);

                await Clients
                  .Group(userName)
                  .ReceiveMessage(userName, stringResponse);
            }

        }
        public async Task SaveMessage(string message)
        {
         
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Message messageModel = new Message() { Date=DateTime.Now,Text= message, UserId=userId};
            _applicationContext.Messages.Add(messageModel);
            await _applicationContext.SaveChangesAsync();





            // db.Messages.AddAsync();
        }

    }
}
