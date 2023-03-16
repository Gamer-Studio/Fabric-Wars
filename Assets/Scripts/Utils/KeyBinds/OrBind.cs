using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FabricWars.Utils.KeyBinds
{
    public class OrBind : Bind
    {
        public OrBind(params IBind[] binds) : base(binds) { }

        public override bool IsKeyPressed(out KeyCode[] res)
        {
            List<KeyCode> list = new();
            foreach (var keybind in binds)
            {
                if (keybind.IsKeyPressed(out KeyCode[] bindKeys))
                {
                    list.AddRange(bindKeys);
                }
            }
            res = list.ToArray();
            return list.Any();
        }

        public override bool IsKeyDown(out KeyCode[] res)
        {
            List<KeyCode> list = new();
            foreach (var keybind in binds)
            {
                if (keybind.IsKeyDown(out KeyCode[] bindKeys))
                {
                    list.AddRange(bindKeys);
                }
            }
            res = list.ToArray();
            return list.Any();
        }
    }
}