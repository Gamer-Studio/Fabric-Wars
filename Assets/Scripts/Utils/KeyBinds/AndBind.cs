using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FabricWars.Utils.KeyBinds
{
    public class AndBind : Bind
    {
        public AndBind(params IBind[] binds) : base(binds) { }

        public override bool IsKeyPressed(out KeyCode[] res)
        {
            List<KeyCode> list = new();
            foreach (var bind in binds)
            {
                if (bind.IsKeyPressed(out var bindKeys))
                {
                    list.AddRange(bindKeys);
                }
                else
                {
                    res = null;
                    return false;
                }
            }
            res = list.ToArray();
            return true;
        }
        
        public override bool IsKeyDown(out KeyCode[] res)
        {
            List<KeyCode> list = new();
            foreach (var bind in binds)
            {
                if (bind.IsKeyDown(out var bindKeys))
                {
                    list.AddRange(bindKeys);
                }
                else
                {
                    res = null;
                    return false;
                }
            }
            res = list.ToArray();
            return true;
        }
    }
}