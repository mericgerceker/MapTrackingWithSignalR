using MapTrackingWithSignalR.API.Hubs;
using MapTrackingWithSignalR.API.Models.Location;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MapTrackingWithSignalR.API.Controllers
{
    [ApiController]
    [Route("locations")]
    public class LocationController
    {
        private readonly IHubContext<LocationHub> _locationHubContext;

        public LocationController(IHubContext<LocationHub> locationHubContext)
        {
            _locationHubContext = locationHubContext;
        }

        [HttpPost]
        public async Task<IActionResult> InsertLocation(LocationModel locationModel)
        {
            await _locationHubContext.Clients.All.SendAsync("ReceiveLocation", locationModel);

            return new OkResult();
        }
    }
}
