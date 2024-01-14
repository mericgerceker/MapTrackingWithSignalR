using System.Text.Json;

class Program
{
    static readonly HttpClient client = new HttpClient();
    static readonly Random random = new Random();

    static async Task Main()
    {
        List<Guid> vehicleIdList = CreateVehicleIdList();

        foreach (Guid vehicleId in vehicleIdList)
        {
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    var latitude = (random.NextDouble() * 180) - 90;
                    var longitude = (random.NextDouble() * 360) - 180;

                    var response = await PostLocationAsync(vehicleId, latitude, longitude);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Vehicle {vehicleId} location updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Error updating location for vehicle {vehicleId}: {response.StatusCode}");
                    }

                    await Task.Delay(3000);
                }
            });
        }

        // Prevent the application from exiting until all tasks are complete.
        await Task.Delay(-1);
    }

    static List<Guid> CreateVehicleIdList()
    {
        var vehicleIdList = new List<Guid>();

        for (int i = 0; i < 50; i++)
        {
            vehicleIdList.Add(Guid.NewGuid());
        }

        return vehicleIdList;
    }

    static async Task<HttpResponseMessage> PostLocationAsync(Guid vehicleId, double latitude, double longitude)
    {
        var location = new
        {
            VehicleId = vehicleId,
            Latitude = latitude,
            Longitude = longitude
        };

        var json = JsonSerializer.Serialize(location);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        string apiBaseUrl = "https://localhost:44308";

        return await client.PostAsync($"{apiBaseUrl}/locations", content);
    }
}