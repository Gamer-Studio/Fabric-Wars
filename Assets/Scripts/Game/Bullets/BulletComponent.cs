using UnityEngine;

namespace FabricWars.Game.Bullets
{
    [CreateAssetMenu(fileName = "new Bullet Component", menuName = "Game/Bullet/Basic bullet component", order = 0)]
    public class BulletComponent : ScriptableObject
    {
        public virtual GameObject Attach(Bullet bullet)
        {
            return null;
        }

        public virtual void Init(GameObject component)
        {
            
        }
    }
}