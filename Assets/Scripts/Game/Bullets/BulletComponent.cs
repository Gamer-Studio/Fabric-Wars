using UnityEngine;

namespace FabricWars.Game.Bullets
{
    public abstract partial class BulletComponent : ScriptableObject
    {
        public abstract GameObject Attach(Bullet bullet);
        public abstract void Release(GameObject component);
    }
}