using UnityEngine;
using UnityEngine.Pool;

namespace FabricWars.Game.Bullets
{
    [CreateAssetMenu(fileName = "new Bullet Prefab Component", menuName = "Game/Bullet/Prefab component", order = 0)]
    public class BulletPrefabComponent : BulletComponent
    {
        // base pool configurations
        [SerializeField] private GameObject componentBase;

        // pool
        private static BulletManager manager => BulletManager.instance;
        private ObjectPool<GameObject> _pool = null;
        private Transform _poolContainer = null;
        
        protected virtual ObjectPool<GameObject> pool
        {
            get => _pool ??= new ObjectPool<GameObject>(
                () =>
                {
                    if (_poolContainer == null)
                    {
                        _poolContainer = new GameObject
                        {
                            name = $"{GetType().Name}_Pool",
                            transform =
                            {
                                parent = manager.transform,
                                position = Vector3.zero,
                                rotation = Quaternion.identity
                            }
                        }.transform;
                    }
                    
                    return Instantiate(componentBase, _poolContainer);
                },
                comp => comp.SetActive(true),
                comp =>
                {
                    comp.SetActive(false);
                    comp.transform.parent = _poolContainer;
                }
            );
            set => _pool = value;
        }


        public override GameObject Attach(Bullet bullet)
        {
            var comp = pool.Get();
            comp.transform.SetParent(bullet.transform);
            comp.transform.position = Vector3.zero;
            
            return comp;
        }

        public override void Release(GameObject component)
        {
            _pool.Release(component);
        }
    }
}