using Microsoft.AspNetCore.SignalR;

namespace GPTshka4.Hubs
{
    public interface IChatClient
    {
        public Task ResiveMessage(string userName,string message);
    }
    public class ChatHub :Hub<IChatClient>
    {
       public async Task JoinChat(string UserName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, UserName);
            await Clients
                .Group(UserName)
                .ResiveMessage("System", $"{UserName} присоединился к чату");
        }



    }
}
