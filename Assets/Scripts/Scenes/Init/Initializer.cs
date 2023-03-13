using UnityEngine;
using UnityEngine.SceneManagement;

namespace FabricWars.Scenes.Init
{
    public class Initializer : MonoBehaviour
    {
        private void Awake()
        {
            General.Initialization();
            SceneManager.LoadScene("Board");
        }
    }
}