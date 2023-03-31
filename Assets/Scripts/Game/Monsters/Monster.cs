using System.Collections;
using UnityEngine;

namespace FabricWars.Game.Monsters
{
    public class Monster : MonoBehaviour
    {
        // Builder
        public const float WaitForCreation = 5;
        private bool _inBuilding = false;
        
        public void StartBuild()
        {
            IEnumerator BuildTimer()
            {
                yield return new WaitForSeconds(WaitForCreation);
                if(_inBuilding) Release();
            }
            
            StartCoroutine(BuildTimer());
            
            _inBuilding = true;
        }

        public void Copy(Monster monster)
        {
            
        }
        
        public void BuildAndSpawn(Transform parent, Vector3 position, int count = 1)
        {
            if(!_inBuilding) return;

            transform.SetParent(parent);
            transform.position = position;
            
            for (var i = 1; i < count; i++)
            {
                var mob = MonsterManager.instance.GetMonsterBase();
                mob.Copy(this);
                mob.transform.SetParent(parent);
                mob.transform.position = position;
            }

            _inBuilding = false;
        }
        
        public void Release()
        {
            
        }
    }
}