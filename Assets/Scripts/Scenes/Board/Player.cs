using System.Collections.Generic;
using FabricWars.Game.Entities;

namespace FabricWars.Scenes.Board
{
    public class Player : Entity
    {
        public static readonly List<Player> players = new();

        protected override void Awake()
        {
            base.Awake();
            
            players.Add(this);
        }

        private void OnDestroy()
        {
            players.Remove(this);
        }
    }
}