using System.Windows;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Wasmachine9000.Game;

public class GameState
{
    // Keep track of navigation
    private Window CurrentWindow;
    private Window PreviousWindow;

    // User stats (coins, highscore)
    private int Coins;
    private int Highscore;

    // User info
    private string Username;
    private int Pincode;

    public GameState()
    {
        
    }

    public void SetCoins(int coins)
    {
        Coins = coins;
    }

    public int GetCoins()
    {
        return Coins;
    }

    public void SetHighscore(int highscore)
    {
        Highscore = highscore;
    }

    public int GetHighscore()
    {
        return Highscore;
    }

    public void SetUsername(string username)
    {
        if (username.Length > 12)
        {
            throw new Exception("Username is too long. Max 12 characters.");
        }

        Username = username;
    }

    public string GetUsername()
    {
        return Username;
    }

    public void SetPincode(int pincode)
    {
        if (pincode is < 1000 or > 9999)
        {
            throw new Exception("Pincode cannot be lower than 1000 and not higher than 9999");
        }

        Pincode = pincode;
    }

    public int GetPincode()
    {
        return Pincode;
    }

    public static string GetGameStateFilePath()
    {
        string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        return Path.Combine(appdataPath, ".wasmachine9000");
    }

    public static GameState LoadGameState()
    {
        GameState gameState = new GameState();
        //check if gamestate file exists
        if (File.Exists(GetGameStateFilePath()))
        {
            // Open gamestate file
            using (StreamReader gameStateReader = new StreamReader(GetGameStateFilePath()))
        {
            
           
                // Read gamestate file
                string gameStateData = gameStateReader.ReadToEnd();
                foreach (string gameStateFileLine in gameStateData.Split("\n"))
                {
                    string[] gameStateEntry = gameStateFileLine.Split(":");

                    switch (gameStateEntry[0])
                    {
                        case "Coins":
                            gameState.SetCoins(int.Parse(gameStateEntry[1]));
                            break;
                        case "Highscore":
                            gameState.SetHighscore(int.Parse(gameStateEntry[1]));
                            break;
                        case "Username":
                            gameState.SetUsername(gameStateEntry[1]);
                            break;
                        case "Pincode":
                            gameState.SetPincode(int.Parse(gameStateEntry[1]));
                            break;
                    }
                }

            }
           

            return gameState;
        }
        
        else
        {
            FileStream creategamestate = File.Create(GetGameStateFilePath());
            LoadGameState();
        }

        return gameState;
    }

    public bool SaveGameState()
    {
        // Get %appdata% folder
        string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        // Open file %appdata%.wasmachine9000
        using (StreamWriter gameStateFile = new StreamWriter(GetGameStateFilePath()))
        {
            // Write all gamestate variables to the file
            gameStateFile.WriteLine("Coins:" + Coins);
            gameStateFile.WriteLine("Highscore:" + Highscore);
            gameStateFile.WriteLine("Username:" + Username);
            gameStateFile.WriteLine("Pincode:" + Pincode);
        }

        return false;
    }
}