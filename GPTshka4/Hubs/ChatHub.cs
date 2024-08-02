using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace GPTshka4.Hubs
{
    public interface IChatClient
    {
        public Task ReceiveMessage(string userName,string message);
    }
    public class ChatHub :Hub<IChatClient>
    {
        private readonly IMemoryCache _cache ;
       public ChatHub(IMemoryCache cache)
        {
            _cache= cache;
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
            Console.WriteLine(message);
            var userName = _cache.Get(Context.ConnectionId).ToString();
            if (userName != null)
            {
                await Clients
                    .Group(userName)
                    .ReceiveMessage(userName, message);
            }
        }


    }
}
