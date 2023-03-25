using System;
using UnityEngine;

namespace FabricWars.Utils.KeyBinds
{
    public interface IKeyBind : IBind
    {
        void Init(KeyCode[] codes);
        KeyCode[] GetKeys();
    }

    public class KeyBind : Bind, IKeyBind
    {
        private KeyCode _key;

        public virtual void Init(KeyCode[] codes) => _key = codes[0];

        public override bool IsKeyPressed(out KeyCode[] res)
        {
            res = new[] { _key };
            return Input.GetKey(_key);
        }
        
        public override bool IsKeyDown (out KeyCode[] res)
        {
            res = new[] { _key };
            return Input.GetKeyDown(_key);
        }

        public virtual KeyCode[] GetKeys() => new[] { _key };
    }
}