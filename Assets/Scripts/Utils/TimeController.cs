using FabricWars.Utils.Singletons;
using System.Collections;
using UnityEngine;

namespace FabricWars.Utils
{
    public class TimeController : LazyDDOLSingletonMonoBehaviour<TimeController>
    {
        float cachedScale;
        public bool paused = false;

        public void Pause()
        {
            if (paused) return;
            paused = true;
            cachedScale = Time.timeScale;
            Time.timeScale = 0;
        }

        public void UnPause()
        {
            if (!paused) return;
            paused = false;
            Time.timeScale = cachedScale;
        }
    }
}