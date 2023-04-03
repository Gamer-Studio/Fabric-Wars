using UnityEngine;
using UnityEngine.Serialization;

namespace FabricWars.Game.Elements
{
    [CreateAssetMenu(fileName = "new Element", menuName = "Game/Element/Basic Element")]
    public partial class Element : ScriptableObject
    {
        public Color color;
        public Sprite mark;
    }
}