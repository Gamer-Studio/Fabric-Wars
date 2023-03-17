using UnityEngine;

namespace FabricWars.Game.Items
{
    [CreateAssetMenu(fileName = "new Item Attribute", menuName = "Game/Item/Basic Item Attribute")]
    public partial class ItemAttribute : ScriptableObject
    {
        public Color color;
    }
}