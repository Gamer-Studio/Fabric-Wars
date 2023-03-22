using System;
using UnityEngine;

namespace FabricWars.Utils
{
    public class ManagerSingleton <T> : MonoBehaviour where T : ManagerSingleton<T>
    {
        public static T instance { get; private set; }

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            
            instance = this as T;
        }
    }
}