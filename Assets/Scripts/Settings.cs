using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace FabricWars
{
    public static class Settings
    {
        public static string dataPath;
        public static readonly Dictionary<string, KeyCode> keyMappings = new ()
        {
            ["elementManager.activateWithSelect"] = KeyCode.LeftShift,
            ["playerManager_openInventory"] = KeyCode.I
        };

        static Settings()
        {
            Debug.Log("Loading Settings");
            dataPath = Path.Combine(Application.dataPath, "Settings");

            if (File.Exists(Path.Combine(dataPath, "Keybinds.json")))
            {

                using var file = File.OpenText(Path.Combine(dataPath, "Keybinds.json"));
                using var reader = new JsonTextReader(file);
                foreach (var (key, token) in (JObject)JToken.ReadFrom(reader))
                {
                    if (token == null || !int.TryParse(token.ToString(), out var i)) continue;

                    if(i < 0) continue;
                    try
                    {
                        keyMappings[key] = (KeyCode)i;
                    }
                    catch (Exception)
                    {
                        Debug.Log($"keybind index {i} is not a valid key");
                    }
                }
            }
        }

        public static void Save()
        {
            File.WriteAllText(Path.Combine(dataPath, "Keybinds.json"), JObject.FromObject(keyMappings).ToString(Formatting.Indented));
        }
    }
}