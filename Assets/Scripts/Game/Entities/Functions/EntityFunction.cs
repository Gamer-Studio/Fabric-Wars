using System.Collections;
using UnityEngine;

namespace FabricWars.Game.Entities.Functions
{
    [CreateAssetMenu(fileName = "new Entity Function", menuName = "Game/Function/Entity Function", order = 0)]
    public class EntityFunction : ScriptableObject
    {
        public virtual IEnumerator GetFunction()
        {
            return null;
        }
    }
}