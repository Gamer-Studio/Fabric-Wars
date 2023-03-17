using UnityEngine;

namespace FabricWars.Game.Elements
{
    [CreateAssetMenu(fileName = "new Element", menuName = "Game/Item/Basic Element")]
    public partial class Element : ScriptableObject
    {
        public Color color;
    }
}