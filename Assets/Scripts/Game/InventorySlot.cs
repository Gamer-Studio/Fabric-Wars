using System;
using FabricWars.Game.Items;

namespace FabricWars.Game
{
    [Serializable]
    public class InventorySlot
    {
        public Item item;
        public int amount;

        public InventorySlot(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        public void DeConstruct(out Item item, out int itemAmount)
        {
            item = this.item;
            itemAmount = amount;
        }

        public static bool IsNullOrEmpty(InventorySlot slot) =>
            slot == null || slot.item == null || slot.item == Item.None || slot.amount == 0;
    }
}