using System;
using System.Collections.Generic;

namespace FabricWars.Game
{
    [Serializable]
    public struct Team
    {
        // static
        public static readonly Dictionary<byte, string> TeamNames = new ()
        {
            [0] = "Player",
            [1] = "Enemy",
        };
        
        public static readonly Team 
            Player = new(0),
            Enemy = new(1);
        
        
        public byte id;

        public Team(byte id)
        {
            this.id = id;
        }

        // utility
        public string Name
        {
            get
            {
                if (!TeamNames.TryGetValue(id, out var name)) name = "None";
                return name;
            }
            set => TeamNames[id] = value;
        }
        public override string ToString() => $"Team_{id}";
        public override int GetHashCode() => id;
        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case null:
                    return false;
                case string name when Name == name:
                    return true;
            }

            if(byte.TryParse(obj.ToString(), out var other)&& other == id) return true;
                
            return base.Equals(obj);
        }

        public static implicit operator byte(Team team) => team.id;
        public static implicit operator Team(byte id) => new (id);
    }
}