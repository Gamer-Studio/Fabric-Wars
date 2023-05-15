using FabricWars.Game.Items;
using FabricWars.Utils.Attributes;
using FabricWars.Utils.Extensions;
using UnityEngine;
using UnityEngine.UI;

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
                if (itemImage != null)
                {
                    if (value != null)
                    {
                        itemImage.sprite = value.sprite;
                        itemImage.color = itemImage.color.A(1);
                    }
                    else
                    {
                        itemImage.sprite = null;
                        itemImage.color = itemImage.color.A(0f / 255);
                    }
                }
            }
        }
        [SerializeField] private Image itemImage;
    }
}