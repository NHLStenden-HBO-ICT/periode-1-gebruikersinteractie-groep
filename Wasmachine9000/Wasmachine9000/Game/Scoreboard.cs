using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Wasmachine9000.Game;

public class Scoreboard
{
    // Although storing this in plaintext is not that good, we do it anyway.
    private static string _pocketbaseUrl = "https://wasmachine.vps.stef1904berg.nl/";

    public Scoreboard()
    {
    }

    public static async void PostScore(string username, int score)
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

    private static async Task<string> PostRequest(string url, Object data)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            string jsonString = JsonSerializer.Serialize(data);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await httpClient.PostAsync(_pocketbaseUrl + url, content);

            if (!response.IsSuccessStatusCode) return response.StatusCode.ToString();
            
            string responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;

        }
    }
}