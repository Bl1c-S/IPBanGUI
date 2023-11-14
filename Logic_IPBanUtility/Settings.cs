using System.Text.Json;

namespace Logic_IPBanUtility;

public class Settings
{
     public string DirrectoryPath { get; set; }
     public string SettingsFilePath { get; set; }
     public string ContextFilePath { get; set; }
     public string KeyNamesFilePath { get; set; }

     public Settings()
     {
          var settings = GetSettings();
          DirrectoryPath = settings.DirrectoryPath;
          SettingsFilePath = settings.SettingsFilePath;
          KeyNamesFilePath = settings.KeyNamesFilePath;
          ContextFilePath = settings.ContextFilePath;

     }
     private Settings(string dirrectoryPath)
     {
          DirrectoryPath = dirrectoryPath;
          SettingsFilePath = Path.Combine(dirrectoryPath, "setting.json");
          ContextFilePath = Path.Combine(dirrectoryPath, "ipban.config");
          KeyNamesFilePath = Path.Combine(dirrectoryPath, "keynames.txt");
     }

     public void SaveSettings(Settings defaultSettings)
     {
          string settingsJson = JsonSerializer.Serialize(defaultSettings);
          File.WriteAllText(defaultSettings.SettingsFilePath, settingsJson);
     }
     public void SaveSettings()
     {
          string settingsJson = JsonSerializer.Serialize(this);
          File.WriteAllText(SettingsFilePath, settingsJson);
     }

     public Settings GetSettings()
     {
          if (DirrectoryPath is null)
               return CreateDefaultSettings();
          var settings = TryLoadSettings();
          if (settings is null)
               return CreateDefaultSettings();
          return settings;
     }

     private Settings? TryLoadSettings()
     {
          var settingsJson = File.ReadAllText(SettingsFilePath);
          var settings = JsonSerializer.Deserialize<Settings>(settingsJson);
          return settings;
     }

     private Settings CreateDefaultSettings()
     {
          DirrectoryPath = Path.Combine("C:\\Program Files\\IPBan");
          var defaultSettings = new Settings(DirrectoryPath);
          SaveSettings(defaultSettings);
          return defaultSettings;
     }

     public void SaveChanged(string dirrectoryPath)
     {
          DirrectoryPath = dirrectoryPath;
          SettingsFilePath = Path.Combine(dirrectoryPath, "setting.json");
          ContextFilePath = Path.Combine(dirrectoryPath, "ipban.config");
          KeyNamesFilePath = Path.Combine(dirrectoryPath, "keynames.txt");
          SaveSettings();
     }
}