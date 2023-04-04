using System.Collections;
using UnityEngine;

namespace FabricWars.Assets.Scripts.UI
{
    public abstract class Dialog<InitData> : MonoBehaviour
    where InitData : struct
    {
        bool wasCursorInvisible = false;

        public virtual void Build(InitData data)
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
}