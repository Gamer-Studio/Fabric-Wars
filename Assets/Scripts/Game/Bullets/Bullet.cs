using System.Collections;
using System.Collections.Generic;
using FabricWars.Utils.Serialization;
using UnityEngine;

namespace FabricWars.Game.Bullets
{
    public class Bullet : MonoBehaviour
    {
        private static BulletManager manager => BulletManager.instance;
        
        // Properties
        public const float WaitForCreation = 1f;
        private bool _inBuilding = false;
        [SerializeField] private List<SerializablePair<BulletComponent, GameObject>> components = new();
        
        internal void StartBuild()
        {
            _inBuilding = true;
            
            IEnumerator BuildTimer()
            {
                yield return new WaitForSeconds(WaitForCreation);
                if(_inBuilding) Release();
                yield break;
            }

            StartCoroutine(BuildTimer());
        }
        
        public Bullet Attach(BulletComponent bulletComponent)
        {
            if(!_inBuilding) return this;
            
            components.Add((bulletComponent, bulletComponent.Attach(this)));
            return this;
        }
        
        public void Build()
        {
            _inBuilding = false;
        }

        public void Release()
        {
            foreach (var (component, obj) in components)
            {
                component.Release(obj);
            }
        }
    }
}