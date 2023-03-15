using System.Collections.Generic;
using FabricWars.Game.Entities.Functions;
using FabricWars.Utils;
using FabricWars.Utils.Serialization;
using UnityEngine;

namespace FabricWars.Game.Entities.Objects
{
    public class EntityObject : MonoBehaviour
    {
        public GaugeInt health;

        public List<SerializablePair<float, EntityFunction>> continuous;
        
        protected virtual void Awake()
        {
        }
        
        // broadcast event interface
        private void OnBreak()
        {
        }
    }
}