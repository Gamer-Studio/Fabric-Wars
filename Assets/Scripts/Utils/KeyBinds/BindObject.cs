using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FabricWars.Utils.KeyBinds
{
    public struct BindOptions
    {
        public bool once;
        public bool onlyDown;

        public static BindOptions defaultOption = new() { once = false, onlyDown = false };
        public static BindOptions downOnly = new() { once = false, onlyDown = true };

        public BindOptions Once(bool value) => new() { onlyDown = onlyDown, once = value };
        public BindOptions OnlyDown(bool value) => new() { onlyDown = value, once = once };
    }

    public class BindObject
    {
        public bool once = false;
        public bool onlyDown = false;
        public int id;

        public IBind bind;
        public Action<KeyCode[], BindObject> callback = (_, _) => { };
        public Action elseCallback = () => { };

        public BindObject(BindOptions options, IBind bind)
        {
            once = options.once;
            onlyDown = options.onlyDown;
            this.bind = bind;
            id = KeyBindManager.instance.binds.Count;
        }
    }
}