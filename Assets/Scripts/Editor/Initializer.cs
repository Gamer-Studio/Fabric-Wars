#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FabricWars.Editor
{
    [InitializeOnLoad]
    public static class Initializer
    {
        static Initializer(){
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                General.Initialization();
            }
        }
    }
}
#endif