using FabricWars.Game.Items;
using FabricWars.Utils.Attributes;
using FabricWars.Utils.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace FabricWars.Scenes.Game
{
    public class InventoryViewerSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerMoveHandler
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
        [SerializeField] private Image backgroundImage;
        public bool enable { get; protected set; }

        public void SetEnable(bool enable)
        {
            this.enable = enable;
            backgroundImage.enabled = enable;
            itemImage.enabled = enable;
        }

        public void OnPointerEnter(PointerEventData data)
        {
        }

        public void OnPointerExit(PointerEventData data)
        {
        }

        private static InventoryViewerSlot selectedSlot = null;
        public void OnPointerDown(PointerEventData data)
        {
            selectedSlot = this;
        }

        public void OnPointerMove(PointerEventData data)
        {
            if (selectedSlot == this)
            {
            }
        }

        public static void OnPointerUp()
        {
            if (selectedSlot != null)
            {
                 
                selectedSlot = null;
            }
        }
    }
}