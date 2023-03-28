using System.Collections.Generic;
using FabricWars.Utils;
using UnityEngine;
using UnityEngine.Pool;

namespace FabricWars.Game.Bullets
{
    public class BulletManager : ManagerSingleton<BulletManager>
    {
        private ObjectPool<Bullet> bulletPool;
        [SerializeField] private GameObject bulletBase;
        
        [SerializeField] private Transform poolBaseContainer;
        public Dictionary<BulletComponent, (Transform container, List<GameObject> pool)> bulletComponentPool = new ();

        protected override void Awake()
        {
            base.Awake();
            
            bulletPool = new ObjectPool<Bullet>(
                () =>
                {
                    var obj = Instantiate(bulletBase);
                    return obj.GetComponent<Bullet>();
                },
                bullet =>
                {
                    
                },
                bullet =>
                {
                    var deconstructComponents = bullet.Release();

                    foreach (var (type, obj) in deconstructComponents)
                    {
                        
                    }
                },
                bullet =>
                {
                    
                },
                true, 10, 10000
                );
        }

        public GameObject GetBulletComponent(BulletComponent bulletComponent)
        {
            return null;
        }
    }
}