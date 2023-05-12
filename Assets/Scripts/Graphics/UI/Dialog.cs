﻿using FabricWars.Utils;
using UnityEngine;

namespace FabricWars.Graphics.UI
{
    public abstract class Dialog : MonoBehaviour
    {
        bool wasCursorInvisible = false;

        protected virtual void Build()
        {
            if (!Cursor.visible) wasCursorInvisible = true;
            Cursor.visible = true;
            gameObject.SetActive(true);
            TimeController.Main.Pause();
        }

        public virtual void Close()
        {
            if (wasCursorInvisible) Cursor.visible = false;
            gameObject.SetActive(false);
            TimeController.Main.UnPause();
        }
    }

    public abstract class Dialog<TInitData> : Dialog
    where TInitData : struct
    {
        public virtual void Build(TInitData data) => base.Build();
    }
}