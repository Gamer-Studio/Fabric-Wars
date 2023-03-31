using FabricWars.Utils;
using UnityEngine;
using UnityEngine.Pool;

namespace FabricWars.Game.Monsters
{
    public class MonsterManager : ManagerSingleton<MonsterManager>
    {
        // pool
        private ObjectPool<Monster> pool;
        private Transform _poolContainer;
        [SerializeField] private GameObject monsterBase;

        protected override void Awake()
        {
            base.Awake();

            pool = new ObjectPool<Monster>(
                () => Instantiate(monsterBase, Vector3.zero, Quaternion.identity, _poolContainer)
                    .GetComponent<Monster>(),
                mob =>
                {
                    mob.gameObject.SetActive(true);
                    mob.StartBuild();
                },
                mob =>
                {
                    mob.gameObject.transform.SetParent(_poolContainer);
                    mob.gameObject.SetActive(false);
                },
                mob => Destroy(mob.gameObject),
                true, 10, 10000
            );
        }
        
        public Monster GetMonsterBase() => pool.Get();
        public void Release(Monster monster) => pool.Release(monster);
    }
}