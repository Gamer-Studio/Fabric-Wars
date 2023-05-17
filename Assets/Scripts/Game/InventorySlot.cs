using System;
using FabricWars.Game.Items;
using FabricWars.Utils;
using FabricWars.Utils.Attributes;
using UnityEngine;

namespace FabricWars.Game
{
    [Serializable]
    public class InventorySlot
    {
        public readonly Inventory inventory;
        private int _inventoryIndex; 
        public Item item;
        [SerializeField, GetSet("amount")] private int _amount;

        public int amount
        {
            get => _amount;
            set
            {
                _amount = value.Range(0, item.maxAmount);
                if (inventory != null) inventory.OnSlotChanged.Invoke(_inventoryIndex);
            }
        }

        public InventorySlot(Item item, int amount)
        {
            this.item = item;
            this._amount = amount;
        }
        
        public InventorySlot(Item item, int amount, Inventory inventory, int index)
        {
            this.inventory = inventory;
            this._inventoryIndex = index;
            this.item = item;
            this._amount = amount;
        }

        public void DeConstruct(out Item item, out int itemAmount)
        {
            item = this.item;
            itemAmount = _amount;
        }

        public static bool IsNullOrEmpty(InventorySlot slot) =>
            slot == null || slot.item == null || slot.item == Item.None || slot.amount == 0;
    }
}