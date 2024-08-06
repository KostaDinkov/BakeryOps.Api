using Microsoft.AspNetCore.SignalR;

namespace BakeryOps.API.Hubs
{
    public class EventHub:Hub

    {
        public async Task UpdateClients()
        {
            
            await Clients.AllExcept(Context.ConnectionId).SendAsync("UpdateOrders");
            
        }
    }
}
