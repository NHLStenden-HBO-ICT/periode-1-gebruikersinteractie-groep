using System;
using System.Collections.Generic;
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

        public Dictionary<string, bool> Cosmetic1 { get; set; } = new Dictionary<string, bool>
        {
            { "Bought", false },
            { "Equipped", false }
        };

        public Dictionary<string, bool> Cosmetic2 { get; set; } = new Dictionary<string, bool>
        {
            { "Bought", false },
            { "Equipped", false }
        };

        public Dictionary<string, bool> Cosmetic3 { get; set; } = new Dictionary<string, bool>
        {
            { "Bought", false },
            { "Equipped", false }
        };

        public Dictionary<string, bool> Cosmetic4 { get; set; } = new Dictionary<string, bool>
        {
            { "Bought", false },
            { "Equipped", false }
        };

        public Dictionary<string, bool> Cosmetic5 { get; set; } = new Dictionary<string, bool>
        {
            { "Bought", false },
            { "Equipped", false }
        };

        public Dictionary<string, bool> Cosmetic6 { get; set; } = new Dictionary<string, bool>
        {
            { "Bought", false },
            { "Equipped", false }
        };

        public bool MusicSound;
        public bool SFXSound;
        public double MainVolume;
        public double MusicVolume;
        public double SFXVolume;

        //Parental control settings
        public DateTime PlayLockedUntil;

        //Playtime in minutes
        public int MaxplayTime;
        public bool PlaytimeControl;
        public int PlaytimePassed;


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
                           MainVolume = 0.5,
                           MusicVolume = 0.5,
                           SFXVolume = 0.5,
                            Cosmetic1 = new Dictionary<string, bool>
                            {
                                { "Bought", false },
                                { "Equipped", false }
                            },
                            Cosmetic2 = new Dictionary<string, bool>
                            {
                                { "Bought", false },
                                { "Equipped", false }
                            },
                            Cosmetic3 = new Dictionary<string, bool>
                            {
                                { "Bought", false },
                                { "Equipped", false }
                            },
                            Cosmetic4 = new Dictionary<string, bool>
                            {
                                { "Bought", false },
                                { "Equipped", false }
                            },
                            Cosmetic5 = new Dictionary<string, bool>
                            {
                                { "Bought", false },
                                { "Equipped", false }
                            },
                            Cosmetic6 = new Dictionary<string, bool>
                            {
                                { "Bought", false },
                                { "Equipped", false }
                            },
                            MusicSound = true,
                            SFXSound = true,
                            PlaytimeControl = false,
                            MaxplayTime = 0,
                            PlaytimePassed = 0,
                            PlayLockedUntil = null
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
                MainVolume = data.MainVolume ?? 0;
                MusicVolume = data.MusicVolume ?? 0;
                SFXVolume = data.SFXVolume ?? 0;
                Cosmetic1 = data.Cosmetic1;
                Cosmetic2 = data.Cosmetic2;
                Cosmetic3 = data.Cosmetic3;
                Cosmetic4 = data.Cosmetic4;
                Cosmetic5 = data.Cosmetic5;
                Cosmetic6 = data.Cosmetic6;
                MusicSound = data.MusicSound ?? true;
                SFXSound = data.SFXSound ?? true;
                PlaytimeControl = data.PlaytimeControl ?? false;
                MaxplayTime = data.MaxplayTime ?? 0;
                PlaytimePassed = data.PlaytimePassed ?? 0;
                PlayLockedUntil = data.PlayLockedUntil ?? DateTime.MinValue;
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

        public Dictionary<string, Dictionary<string, bool>> GetAllCosmetics()
        {
            Dictionary<string, Dictionary<string, bool>> allCosmetics =
                new Dictionary<string, Dictionary<string, bool>>();
            allCosmetics.Add("Cosmetic1", Cosmetic1);
            allCosmetics.Add("Cosmetic2", Cosmetic2);
            allCosmetics.Add("Cosmetic3", Cosmetic3);
            allCosmetics.Add("Cosmetic4", Cosmetic4);
            allCosmetics.Add("Cosmetic5", Cosmetic5);
            allCosmetics.Add("Cosmetic6", Cosmetic6);

            return allCosmetics;
        }

        public void UpdateCosmetic(string cosmeticName, string propertyName, bool value)
        {
            switch (cosmeticName)
            {
                case "Cosmetic1":
                    if (propertyName == "Bought")
                    {
                        Cosmetic1["Bought"] = value;
                    }
                    else if (propertyName == "Equipped")
                    {
                        Cosmetic1["Equipped"] = value;
                        if (value == true)
                        {
                            Cosmetic2["Equipped"] = false;
                            Cosmetic3["Equipped"] = false;
                            Cosmetic4["Equipped"] = false;
                            Cosmetic5["Equipped"] = false;
                            Cosmetic6["Equipped"] = false;
                        }
                    }

                    break;

                case "Cosmetic2":
                    if (propertyName == "Bought")
                    {
                        Cosmetic2["Bought"] = value;
                    }
                    else if (propertyName == "Equipped")
                    {
                        Cosmetic2["Equipped"] = value;
                        if (value == true)
                        {
                            Cosmetic1["Equipped"] = false;
                            Cosmetic3["Equipped"] = false;
                            Cosmetic4["Equipped"] = false;
                            Cosmetic5["Equipped"] = false;
                            Cosmetic6["Equipped"] = false;
                        }
                    }

                    break;

                case "Cosmetic3":
                    if (propertyName == "Bought")
                    {
                        Cosmetic3["Bought"] = value;
                    }
                    else if (propertyName == "Equipped")
                    {
                        Cosmetic3["Equipped"] = value;
                        if (value == true)
                        {
                            Cosmetic1["Equipped"] = false;
                            Cosmetic2["Equipped"] = false;
                            Cosmetic4["Equipped"] = false;
                            Cosmetic5["Equipped"] = false;
                            Cosmetic6["Equipped"] = false;
                        }
                    }

                    break;

                case "Cosmetic4":
                    if (propertyName == "Bought")
                    {
                        Cosmetic4["Bought"] = value;
                    }
                    else if (propertyName == "Equipped")
                    {
                        Cosmetic4["Equipped"] = value;
                        if (value == true)
                        {
                            Cosmetic1["Equipped"] = false;
                            Cosmetic2["Equipped"] = false;
                            Cosmetic3["Equipped"] = false;
                            Cosmetic5["Equipped"] = false;
                            Cosmetic6["Equipped"] = false;
                        }
                    }

                    break;

                case "Cosmetic5":
                    if (propertyName == "Bought")
                    {
                        Cosmetic5["Bought"] = value;
                    }
                    else if (propertyName == "Equipped")
                    {
                        Cosmetic5["Equipped"] = value;
                        if (value == true)
                        {
                            Cosmetic1["Equipped"] = false;
                            Cosmetic2["Equipped"] = false;
                            Cosmetic3["Equipped"] = false;
                            Cosmetic4["Equipped"] = false;
                            Cosmetic6["Equipped"] = false;
                        }
                    }

                    break;

                case "Cosmetic6":
                    if (propertyName == "Bought")
                    {
                        Cosmetic6["Bought"] = value;
                    }
                    else if (propertyName == "Equipped")
                    {
                        Cosmetic6["Equipped"] = value;
                        if (value == true)
                        {
                            Cosmetic1["Equipped"] = false;
                            Cosmetic2["Equipped"] = false;
                            Cosmetic3["Equipped"] = false;
                            Cosmetic4["Equipped"] = false;
                            Cosmetic5["Equipped"] = false;
                        }
                    }

                    break;

                default:
                    // Handle unknown cosmetic names
                    break;
            }

            // Save the updated game state
            SaveGameState();
        }

        public bool CheckRemainingPlaytime()
        {
            //check if the remaining time is bigger then or equal to the playtimelimit
            if (App.GameState.PlaytimePassed >= App.GameState.MaxplayTime)
            {
                return false;
            }

            return true;
        }

        public bool CheckWaitTimePassed()
        {
            return true;
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

        public static string GetGameStateFilePath()
        {
            string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appdataPath, ".wasmachine9000.yaml");
        }

        public bool SaveGameState()
        {
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
                    MainVolume = MainVolume,
                    MusicVolume = MusicVolume,
                    SFXVolume = SFXVolume,
                    Cosmetic1 = Cosmetic1,
                    Cosmetic2 = Cosmetic2,
                    Cosmetic3 = Cosmetic3,
                    Cosmetic4 = Cosmetic4,
                    Cosmetic5 = Cosmetic5,
                    Cosmetic6 = Cosmetic6,
                    MusicSound = MusicSound,
                    SFXSound = SFXSound,
                    PlaytimeControl = PlaytimeControl,
                    MaxplayTime = MaxplayTime,
                    PlaytimePassed = PlaytimePassed,
                    PlayLockedUntil = PlayLockedUntil
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
    public bool? PlaytimeControl { get; set; }
    public int? Coins { get; set; }
    public int? Highscore { get; set; }
    public string? Username { get; set; }
    public int? Pincode { get; set; }
    public Dictionary<string, bool> Cosmetic1 { get; set; }
    public Dictionary<string, bool> Cosmetic2 { get; set; }
    public Dictionary<string, bool> Cosmetic3 { get; set; }
    public Dictionary<string, bool> Cosmetic4 { get; set; }
    public Dictionary<string, bool> Cosmetic5 { get; set; }
    public Dictionary<string, bool> Cosmetic6 { get; set; }
    public bool? MusicSound { get; set; }
    public bool? SFXSound { get; set; }
    public int? MaxplayTime { get; set; }
    public int? PlaytimePassed { get; set; }
    public DateTime? PlayLockedUntil { get; set; }
    public double? MainVolume { get; set; }
    public double? MusicVolume { get; set; }
    public double? SFXVolume { get;set; }
}