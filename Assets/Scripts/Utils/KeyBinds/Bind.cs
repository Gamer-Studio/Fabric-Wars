using System;
using UnityEngine;

namespace FabricWars.Utils.KeyBinds
{
    public interface IBind
    {
        bool IsKeyPressed(out KeyCode[] res);
        bool IsKeyDown(out KeyCode[] res);
    }

    public abstract class Bind : IBind
    {
        public IBind[] binds;

        public Bind(params IBind[] binds) => this.binds = binds;

        public abstract bool IsKeyPressed(out KeyCode[] res);
        public abstract bool IsKeyDown(out KeyCode[] res);
    }
    
    [Flags]
    public enum GetKeyType
    {
        Pressed = 1 << 0,
        Down = 1 << 1,
        Up = 1 << 2
    }
}