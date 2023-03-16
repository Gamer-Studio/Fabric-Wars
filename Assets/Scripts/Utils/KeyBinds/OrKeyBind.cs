using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FabricWars.Utils.KeyBinds
{
    public class OrKeyBind : KeyBind
    {
        KeyCode[] keys;

        public override void Init(KeyCode[] codes) => keys = codes;

        public override bool IsKeyPressed(out KeyCode[] res)
        {
            var list = keys.Where(Input.GetKey).ToList();
            res = list.ToArray();
            return list.Any();
        }
        
        public override bool IsKeyDown(out KeyCode[] res)
        {
            var list = keys.Where(Input.GetKeyDown).ToList();
            res = list.ToArray();
            return list.Any();
        }

        public override KeyCode[] GetKeys() => keys;
    }
}