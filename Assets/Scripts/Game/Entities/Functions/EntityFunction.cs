using System.Collections;
using UnityEngine;

namespace FabricWars.Game.Entities.Functions
{
    public abstract class EntityFunction : ScriptableObject
    {
        public abstract IEnumerator GetFunction();
    }
}