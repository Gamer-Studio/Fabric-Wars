using FabricWars.Utils;
using UnityEngine;

namespace FabricWars.Game.Objects
{
    public class Structure : MonoBehaviour
    {
        public GaugeInt health;
        
        protected virtual void Awake()
        {
        }
        
        // broadcast event interface
        private void OnBreak()
        {
        }
    }
}