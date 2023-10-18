﻿using System;
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
    private string _pocketbaseUrl = "http://wasmachine.vps.stef1904berg.nl/";

    public Scoreboard()
    {
    }

    public List<ScoreboardItem> GetScoreboard()
    {
        string apiUrl = "api/collections/scoreboard/records?perPage=6&sort=-score&fields=username,score";

        using (HttpClient client = new HttpClient())
        {
            var response = client.GetAsync(this._pocketbaseUrl + apiUrl).GetAwaiter().GetResult();
            string jsonContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            ScoreboardResult? scoreboardResult = JsonSerializer.Deserialize<ScoreboardResult>(jsonContent);

            return scoreboardResult?.items;
        }
    }

    public void PostScore(string username, int score)
    {
        var jsonContent = new
        {
            username = username,
            score = score,
            password = "9000wasmachines"
        };
        PostRequest("api/collections/scoreboard/records", jsonContent);
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