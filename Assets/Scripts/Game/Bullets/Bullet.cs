using System.Collections.Generic;
using FabricWars.Utils.Serialization;
using UnityEngine;

namespace FabricWars.Game.Bullets
{
    public class Bullet : MonoBehaviour
    {
        // Builder
        public const float WaitForCreation = 1f;
        private static BulletManager manager => BulletManager.instance;
        
        [SerializeField] private List<SerializablePair<BulletComponent, GameObject>> components = new();

        public Bullet Attach(BulletComponent bulletComponent)
        {
            components.Add((bulletComponent, bulletComponent.Attach(this)));
            return this;
        }
        
        public void Build()
        {
        }

        public List<SerializablePair<BulletComponent, GameObject>> Release()
        {
            foreach (var (component, obj) in components)
            {
                component.Init(obj);
            }
            return components;
        }
    }
}