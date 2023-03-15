using FabricWars.Game.Entities.Objects;
using UnityEngine;

namespace FabricWars.Game.Entities.Functions
{
    [CreateAssetMenu(fileName = "new Object Function", menuName = "Game/Function/default function", order = 0)]
    public class EntityFunction : ScriptableObject
    {
        public virtual void Run(EntityObject obj)
        {
        }
    }
}