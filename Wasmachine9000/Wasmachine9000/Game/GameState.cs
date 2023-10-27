using System;
using System.IO;
using System.Windows;
using YamlDotNet.Serialization;

namespace Wasmachine9000.Game
{
    public class GameState
    {
        // Keep track of navigation
        public Window CurrentWindow;
        public Window PreviousWindow;

        // User stats (coins, highscore)
        private int Coins;
        private int Highscore;

        // User info
        private string Username;
        private int Pincode;

        private bool Cosmetic1;

        public bool MusicSound;
        public bool SFXSound;

        //Parental control settings

        //Playtime in minutes
        public int MaxplayTime;
        public bool PlaytimeControl;

        // Read and parse the YAML file.
        public GamestateData ReadYamlFile(string filePath)
        {
            GamestateData gamestateData = new GamestateData();

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    var deserializer = new Deserializer();
                    gamestateData = deserializer.Deserialize<GamestateData>(reader);

                    if (gamestateData == null)
                    {
                        gamestateData = new GamestateData
                        {
                            Coins = 0,
                            Highscore = 0,
                            Username = "",
                            Pincode = 0,
                            Cosmetic1 = false,
                            MusicSound = true,
                            SFXSound = true
                        };
                    }

                    return gamestateData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public GameState()
        {
            // Load game state from the YAML file
            var data = ReadYamlFile(GetGameStateFilePath());
            if (data != null)
            {
                Coins = data.Coins ?? 0;
                Highscore = data.Highscore ?? 0;
                Username = data.Username ?? "";
                Pincode = data.Pincode ?? 0000;
                Cosmetic1 = data.Cosmetic1;
                MusicSound = data.MusicSound ?? true;
                SFXSound = data.SFXSound ?? true;
                SaveGameState();
            }
        }

        public void SetCoins(int coins)
        {
            Coins = coins;
            SaveGameState();
        }

        public int GetCoins()
        {
            return Coins;
        }

        public void SetHighscore(int highscore)
        {
            Highscore = highscore;
            SaveGameState();
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
            SaveGameState();
        }

        public string GetUsername()
        {
            return Username;
        }

        public void SetPincode(int pincode)
        {
            if (pincode < 1000 || pincode > 9999)
            {
                throw new Exception("Pincode cannot be lower than 1000 and not higher than 9999");
            }

            Pincode = pincode;
            SaveGameState();
        }

        public int GetPincode()
        {
            return Pincode;
        }

        public void SetCosmeticStatus(string cosmeticName, bool status)
        {
            GamestateData gamestateData = ReadYamlFile(GetGameStateFilePath());

            if (gamestateData.Cosmetic1)
            {
                gamestateData.Cosmetic1 = status;
            }
            else
            {
                // Cosmetic not found; you can handle this case as needed.
            }

            SaveGameState();
        }

        public bool GetCosmeticStatus(string cosmeticName)
        {
            GamestateData gamestateData = ReadYamlFile(GetGameStateFilePath());

            if (gamestateData != null)
            {
                switch (cosmeticName)
                {
                    case "Cosmetic1":
                        return gamestateData.Cosmetic1;
                    // case "Cosmetic2":
                    //     return gamestateData.Cosmetic2;
                    // case "Cosmetic3":
                    //     return gamestateData.Cosmetic3;
                    // case "Cosmetic4":
                    //     return gamestateData.Cosmetic4;
                    // case "Cosmetic5":
                    //     return gamestateData.Cosmetic5;
                    // case "Cosmetic6":
                    //     return gamestateData.Cosmetic6;
                    default:
                        throw new ArgumentException("Invalid cosmetic name.");
                }
            }

            return false;
        }

        public static string GetGameStateFilePath()
        {
            string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appdataPath, ".wasmachine9000.yaml");
        }

        public bool SaveGameState()
        {
            // Get %appdata% folder
            string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Open file %appdata%.wasmachine9000.yaml
            using (StreamWriter gameStateFile = new StreamWriter(GetGameStateFilePath()))
            {
                var serializer = new Serializer();
                var GamestateData = new GamestateData
                {
                    Coins = Coins,
                    Highscore = Highscore,
                    Username = Username,
                    Pincode = Pincode,
                    Cosmetic1 = Cosmetic1,
                    MusicSound = MusicSound,
                    SFXSound = SFXSound
                };
                serializer.Serialize(gameStateFile, GamestateData);
            }

            return false;
        }

        public static GameState LoadGameState()
        {
            GameState gameState = new GameState();
            // Check if game state file exists
            if (File.Exists(GetGameStateFilePath()))
            {
                // Open game state file
                using (StreamReader gameStateReader = new StreamReader(GetGameStateFilePath()))
                {
                    // Read game state file
                    var data = gameState.ReadYamlFile(GetGameStateFilePath());
                }

                return gameState;
            }
            else
            {
                // Create yaml file
                FileStream creategamestate = File.Create(GetGameStateFilePath());
                gameState = new GameState();
                // Fill it with default data
                gameState.SaveGameState();
                return LoadGameState();
            }
        }
    }
}

public class GamestateData
{
    public int? Coins { get; set; }
    public int? Highscore { get; set; }
    public string? Username { get; set; }
    public int? Pincode { get; set; }
    public bool Cosmetic1 { get; set; }
    public bool? MusicSound { get; set; }
    public bool? SFXSound { get; set; }
}