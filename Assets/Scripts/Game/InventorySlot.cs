using System;
using FabricWars.Game.Items;
using FabricWars.Utils.Attributes;
using UnityEngine;

namespace FabricWars.Game
{
    [Serializable]
    public class InventorySlot
    {
        public Item itemType;
        public int maxCount;
        [SerializeField, GetSet("count")] private int _count;

        public int count
        {
            get => _count;
            set
            {
                if (value <= 0) _count = 0;
                else if (value >= maxCount) _count = maxCount;
                else _count = value;
            }
        }

        public InventorySlot(Item item, int maxCount, int count)
        {
            itemType = item;
            this.maxCount = maxCount;
            this.count = count;
        }

        public static InventorySlot operator +(InventorySlot slot, int count)
        {
            slot.count += count;

            return slot;
        }

        public static InventorySlot operator -(InventorySlot slot, int count)
        {
            slot.count -= count;
            return slot;
        }

        public static InventorySlot operator *(InventorySlot slot, int multiplier)
        {
            slot.count *= multiplier;
            return slot;
        }

        public void DeConstruct(out Item item, out int itemCount)
        {
            item = itemType;
            itemCount = this.count;
        }
    }
}