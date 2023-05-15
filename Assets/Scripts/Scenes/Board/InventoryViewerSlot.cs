using FabricWars.Game.Items;
using FabricWars.Utils.Attributes;
using UnityEngine;

namespace FabricWars.Scenes.Board
{
    public class InventoryViewerSlot : MonoBehaviour
    {
        [SerializeField, GetSet("item")] private Item _item;

        public Item item
        {
            get => _item;
            set
            {
                _item = value;
            }
        }
    }
}