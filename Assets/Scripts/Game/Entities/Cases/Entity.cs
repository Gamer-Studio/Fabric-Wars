using System.Collections.Generic;
using FabricWars.Game.Entities.Functions;
using FabricWars.Utils.Serialization;
using UnityEngine;

// Cases => Types
namespace FabricWars.Game.Entities.Cases
{
    [CreateAssetMenu(fileName = "new Entity", menuName = "Game/Entity/Basic Entity", order = 0)]
    public partial class Entity : ScriptableObject
    {
        // stats
        public int maxHealth;
        
        // functions
        public List<SerializablePair<float, EntityFunction>> update;

        public List<EntityFunction> onCreate;
        public List<EntityFunction> onUse;
        public List<EntityFunction> onBreak;

        public List<EntityFunction> initFuncs;
    }
}