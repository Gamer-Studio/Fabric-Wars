using System;

namespace FabricWars.Game
{
    [Serializable]
    public struct Team
    {
        public static readonly Team 
            Player = new(0),
            Enemy = new(1);
        
        public byte id;

        public Team(byte id)
        {
            this.id = id;
        }

        public override string ToString() => $"Team_{id}";
        public override int GetHashCode() => id;
        public override bool Equals(object obj)
        {
            if(byte.TryParse(obj.ToString(), out var other)&& other == id) return true;
                
            return base.Equals(obj);
        }

        public static implicit operator byte(Team team) => team.id;
        public static implicit operator Team(byte id) => new (id);
    }
}