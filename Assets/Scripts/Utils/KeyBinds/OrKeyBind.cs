using System;
using System.Linq;
using UnityEngine;

namespace FabricWars.Utils.KeyBinds
{
    [Serializable]
    public class OrKeyBind : KeyBind
    {
        [SerializeField] private KeyCode[] _keys;

        public override void Init(KeyCode[] codes) => _keys = codes;

        public override bool IsKeyPressed(out KeyCode[] res)
        {
            var list = _keys.Where(Input.GetKey).ToList();
            res = list.ToArray();
            return list.Any();
        }

        public override bool IsKeyDown(out KeyCode[] res)
        {
            var list = (from key in _keys where Input.GetKeyDown(key) select key).ToArray();
            foreach (var keyCode in _keys)
            {
                Debug.Log(keyCode);
            }
            res = list;
            return list.Any();
        }

        public override KeyCode[] GetKeys() => _keys;
    }
}