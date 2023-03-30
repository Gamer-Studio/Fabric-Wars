using FabricWars.Utils;
using UnityEngine;
using UnityEngine.Pool;

namespace FabricWars.Game.Bullets
{
    public class BulletManager : ManagerSingleton<BulletManager>
    {
        [SerializeField] private GameObject bulletBase;

        public ObjectPool<Bullet> bulletPool { get; private set; }
        private Transform _bulletBaseContainer;

        protected override void Awake()
        {
            base.Awake();

            _bulletBaseContainer = new GameObject
            {
                name = "Bullet Base Pool",
                transform =
                {
                    parent = transform,
                    position = Vector3.zero,
                    rotation = Quaternion.identity
                }
            }.transform;
            
            bulletPool = new ObjectPool<Bullet>(
                () =>
                {
                    var obj = Instantiate(bulletBase);
                    return obj.GetComponent<Bullet>();
                },
                bullet =>
                {
                    bullet.gameObject.SetActive(true);
                    bullet.StartBuild();
                },
                bullet =>
                {
                    bullet.transform.SetParent(_bulletBaseContainer);
                    bullet.Release();
                },
                bullet =>
                {
                    
                },
                true, 10, 10000
                );
        }
    }
}