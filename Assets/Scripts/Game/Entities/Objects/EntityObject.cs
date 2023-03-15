using FabricWars.Game.Entities.Cases;
using FabricWars.Utils;
using UnityEngine;

namespace FabricWars.Game.Entities.Objects
{
    public class EntityObject : MonoBehaviour
    {
        public Entity type;
        public GaugeInt health;
        
        protected virtual void Awake()
        {
            if (!type)
            {
                return;
            }
        }
        
        // broadcast event interface
        private void OnBreak()
        {
        }

        public void Init()
        {
            if(type) type.initFuncs.ForEach(func => func.Run(this));
            
        }
    }
}