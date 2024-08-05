using apiTest.Models;
using GPTshka4.Models.YandexGPTModels;
using GPTshka4.Source;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Serilog;

namespace GPTshka4.Hubs
{
    public interface IChatClient
    {
        public Task ReceiveMessage(string userName,string message);
    }
    public class ChatHub :Hub<IChatClient>
    {
       private readonly YandexGPTService _yandexGPTService;

       private readonly IMemoryCache _cache ;
       public ChatHub(IMemoryCache cache, IHttpClientFactory httpClientFactory, YandexGPTSettings yandexGPTSettings)
        {
            _yandexGPTService = new YandexGPTService(httpClientFactory, yandexGPTSettings);
            _cache = cache;
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

            Console.WriteLine("Aboba");
            Console.WriteLine(JsonConvert.SerializeObject(response));

            var userName = _cache.Get(Context.ConnectionId).ToString();
            if (userName != null)
            {

                //await Clients
                //    .Group(userName)
                //    .ReceiveMessage(userName, message);

                await Clients
                  .Group(userName)
                  .ReceiveMessage(userName, response.result.alternatives[0].message.text);
            }
        }


    }
}
