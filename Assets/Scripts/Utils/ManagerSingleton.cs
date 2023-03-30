using UnityEngine;

namespace FabricWars.Utils
{
    public class ManagerSingleton<T> : MonoBehaviour where T : ManagerSingleton<T>
    {
        private static T _instance;

        public static T instance => _instance != null
            ? _instance
            : _instance = new GameObject
            {
                name = $"ManagerSingleton_{typeof(T).Name}",
                transform =
                {
                    position = Vector3.zero,
                    rotation = Quaternion.identity
                }
            }.AddComponent<T>();

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
                return;
            }

            _instance = this as T;
        }
    }
}