using UnityEngine;

namespace FabricWars.Utils
{
    public class Singleton <T> : MonoBehaviour where T : Singleton<T>
    {
        
    }
}