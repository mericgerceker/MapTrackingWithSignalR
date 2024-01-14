using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace MapTrackingWithSignalR.API.Hubs
{
    public class LocationHub : Hub
    {
        public async override Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            string connectedUser = Context.User?.Identity?.Name ?? "Unknown";

            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            string? exceptionMessage = exception?.Message;

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(Guid vehicleId, string message)
        {
            await Clients.Groups(vehicleId.ToString()).SendAsync("ReceiveMessage", message);
        }
    }
}
