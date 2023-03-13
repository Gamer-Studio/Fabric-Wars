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
            EditorApplication.playModeStateChanged += state =>
            {
                if (state == PlayModeStateChange.EnteredPlayMode && SceneManager.GetActiveScene().name != "Init")
                {
                    General.Initialization();
                }
            };
        }
    }
}
#endif