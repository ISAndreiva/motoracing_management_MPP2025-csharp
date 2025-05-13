using System.Net.Http.Headers;
using ConcursMotociclism.domain;
using Newtonsoft.Json;

internal class Program
{
    static HttpClient _client = new HttpClient();
    const string basePath = "http://127.0.0.1:8080/rest/races";
    
    public static async Task Main(string[] args)
    {   
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        try
        {
            await TestGetAll();
            Console.WriteLine("");
            await TestGet();
            Console.WriteLine("");
            var id = await TestPost();
            Console.WriteLine("");
            await Console.In.ReadLineAsync();
            await TestPut(id);
            Console.WriteLine("");
            await Console.In.ReadLineAsync();
            await TestDelete(id);
            
            Console.WriteLine("All tests passed!");


        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Server is not running!");
        }
    }

    private static async Task TestGetAll()
    {
        var response = await _client.GetAsync(basePath);
        switch (response.StatusCode)
        {
            case System.Net.HttpStatusCode.OK:
                var content = await response.Content.ReadAsStringAsync();
                var races = JsonConvert.DeserializeObject<List<Race>>(content);
                Console.WriteLine("All received races: (make sure there are 11)");
                foreach (var race in races)
                {
                    Console.WriteLine(race.RaceName);
                }
                break;
            case System.Net.HttpStatusCode.NotFound:
                Console.WriteLine("Not Found");
                break;
        }
    }

    private static async Task TestGet()
    {
        var response = await _client.GetAsync($"{basePath}/4afa2356-c89f-468b-a118-a162cc1758d4");
        switch (response.StatusCode)
        {
            case System.Net.HttpStatusCode.OK:
                var content = await response.Content.ReadAsStringAsync();
                var race = JsonConvert.DeserializeObject<Race>(content);
                if (race.RaceName != "Italian Grand Prix")
                    throw new Exception("Something went wrong");
                Console.WriteLine("Get one race worked!");
                break;
            case System.Net.HttpStatusCode.NotFound:
                throw new Exception("Something went wrong");
        }
        response = await _client.GetAsync($"{basePath}/00000000-0000-0000-0000-000000000000");
        if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
            throw new Exception("Something went wrong");
            
        response = await _client.GetAsync($"{basePath}/000");
        if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
            throw new Exception("Something went wrong");
        Console.WriteLine("Malformed get requests responded corectly!");
    }
    
    private static async Task<Guid> TestPost()
    {
        var race = new Race(Guid.Empty, "Test race C#", 765);
        var content = new StringContent(JsonConvert.SerializeObject(race), System.Text.Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(basePath, content);
        var id = Guid.Empty;
        switch (response.StatusCode)
        {
            case System.Net.HttpStatusCode.Created:
                var responseString = await response.Content.ReadAsStringAsync();
                id = Guid.Parse(responseString.Trim("\"".ToCharArray()));
                Console.WriteLine("Added race with id: " + id);
                break;
            case System.Net.HttpStatusCode.InternalServerError:
                Console.WriteLine("Internal server error at adding");
                break;
            default:
                Console.WriteLine("Unknown error when adding");
                break;
        }
        content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
        response = await _client.PostAsync(basePath, content);
        if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
            throw new Exception("Something went wrong");
        Console.WriteLine("Malformed post request responded correctly!");
        return id;
    }

    private static async Task TestPut(Guid id)
    {
        var race = new Race(id, "Test race C# modified", 250);
        var content = new StringContent(JsonConvert.SerializeObject(race), System.Text.Encoding.UTF8, "application/json");
        var response = await _client.PutAsync(basePath + "/" + id, content);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            throw new Exception("Something went wrong at updating");
        Console.WriteLine("Put worked!");
        
        response = await _client.PutAsync(basePath + "/00000000-0000-0000-0000-000000000000", content);
        if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
            throw new Exception("Something went wrong at updating with malformed id");

        race.Id = Guid.NewGuid();
        content = new StringContent(JsonConvert.SerializeObject(race), System.Text.Encoding.UTF8, "application/json");
        response = await _client.PutAsync(basePath + "/" + race.Id, content);
        if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
            throw new Exception("Something went wrong at updating non existing race");
        
        Console.WriteLine("Malformed put request responded correctly!");
        
    }

    private static async Task TestDelete(Guid id)
    {
        var response = await _client.DeleteAsync(basePath + "/" + id);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            throw new Exception("Something went wrong at deleting");
        
        response = await _client.DeleteAsync(basePath + "/" + "000");
        if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
            throw new Exception("Something went wrong at deleting with malformed id");
        
        response = await _client.DeleteAsync(basePath + "/" + "00000000-0000-0000-0000-000000000000");
        if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
            throw new Exception("Something went wrong at deleting race that doesn't exist");
        
        Console.WriteLine("Delete worked!");
    }
}