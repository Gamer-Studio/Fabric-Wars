using System.Collections.Generic;
using UnityEngine;

namespace FabricWars
{
    public sealed class Settings
    {
        public static Settings instance { get; } = new ();
        
        public readonly Dictionary<string, KeyCode> keyMappings = new ();

        public Settings()
        {
            Debug.Log("Loading Settings");
            //var keyData = JsonUtility.FromJson<SettingsJson>(File.ReadAllText("Settings.json"));
        }

        ~Settings()
        {
            Debug.Log("Unloading Settings");

            if (!Application.isEditor)
            {
                
            }
            else
            {
                Debug.Log("Not saving key mappings");
            }
        }
    }
}