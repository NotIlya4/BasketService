using System.Text;
using Newtonsoft.Json.Linq;

namespace IntegrationTests.Fixture;

public class BasketControllerClient
{
    private readonly HttpClient _client;

    public BasketControllerClient(HttpClient client)
    {
        _client = client;
    }

    public async Task Insert(JObject basket)
    {
        var content = new StringContent(basket.ToString(), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("baskets", content);
        response.EnsureSuccessStatusCode();
    }

    public async Task<JObject> Get(string userId)
    {
        var response = await _client.GetAsync($"baskets/user/id/{userId}");
        response.EnsureSuccessStatusCode();
        return JObject.Parse(await response.Content.ReadAsStringAsync());
    }
}