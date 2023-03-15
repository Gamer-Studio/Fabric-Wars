using FabricWars.Utils;
using UnityEngine;

namespace FabricWars.Game.Entities
{
    public class Entity : MonoBehaviour
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