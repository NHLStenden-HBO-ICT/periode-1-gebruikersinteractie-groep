using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Wasmachine9000.Game;

public class ScoreboardItem
{
    public int score { get; set; }
    public string username { get; set; }
}

public class ScoreboardResult
{
    public List<ScoreboardItem> items { get; set; }
    public int page { get; set; }
    public int perPage { get; set; }
    public int totalItems { get; set; }
    public int totalPages { get; set; }
}

public class Scoreboard
{
    // Although storing this in plaintext is not that good, we do it anyway.
    private string _pocketbaseUrl = "https://wasmachine.vps.stef1904berg.nl/";

    public Scoreboard()
    {
    }

    public async Task<List<ScoreboardItem>> GetScoreboard()
    {
        string apiUrl = "api/collections/scoreboard/records?perPage=6&sort=-score&fields=username,score";

        using (HttpClient client = new HttpClient())
        {
            var response = await client.GetAsync(this._pocketbaseUrl + apiUrl);
            string jsonContent = await response.Content.ReadAsStringAsync();
            ScoreboardResult? scoreboardResult = JsonSerializer.Deserialize<ScoreboardResult>(jsonContent);

            return scoreboardResult?.items;
        }
    }

    public async void PostScore(string username, int score)
    {
        var jsonContent = new
        {
            username = username,
            score = score,
            password = "wasmachine9000"
        };
        string response = await PostRequest("api/collections/scoreboard/records", jsonContent);
        Console.WriteLine(response);
    }

    private async Task<string> PostRequest(string url, Object data)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            string jsonString = JsonSerializer.Serialize(data);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await httpClient.PostAsync(this._pocketbaseUrl + url, content);

            if (!response.IsSuccessStatusCode) return response.StatusCode.ToString();

            string responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
    }
}