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
        }

        ~Settings()
        {
            Debug.Log("Unloading Settings");
        }
    }
}