using FabricWars.Utils;
using UnityEngine;

namespace FabricWars.Game.Entities
{
    public partial class Entity : MonoBehaviour
    {
        
        [Header("Entity Configuration")]
        public GaugeInt health = new(0, 10, 10);
        public Team team = Team.Player;
        
        private void Awake()
        {

        }

        private void Start()
        {
        }

        // broadcast event interface
        private void OnBreak()
        {
        }
    }
}