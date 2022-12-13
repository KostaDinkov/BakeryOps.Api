using Microsoft.AspNetCore.SignalR;
namespace Orders.Hubs
{
    public class EventHub:Hub

    {
        public async Task UpdateClients(string user, string message)
        {
            await Clients.All.SendAsync("UpdateOrders", user, message);
        }
    }
}
