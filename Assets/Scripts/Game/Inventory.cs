using System;
using FabricWars.Game.Items;
using UnityEngine;
using UnityEngine.Events;

namespace FabricWars.Game
{
    public class Inventory : MonoBehaviour
    {
        public UnityEvent<Item, int> onDrop;
        public UnityEvent<Item, int> onAdd;
        
        public InventorySlot[] slots = new InventorySlot[10];

        public int length
        {
            get => slots.Length;
            set
            {
                switch (value)
                {
                    case var _ when value <= 0 || value == slots.Length: return;
                    case var _ when value > slots.Length: Array.Resize(ref slots, value); return;
                    case var _ when value < slots.Length:
                    {
                        return;
                    }
                }
            }
        }

        private void Awake()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item">추가할 아이템의 종류</param>
        /// <param name="amount">추가할 아이템의 개수</param>
        /// <returns>인벤토리에 더 이상 아이템을 추가할 수 없을 경우 그만큼 반환</returns>
        public int TryAddItem(Item item, int amount)
        {
            for (var i = 0; i < slots.Length; i++)
            {
                var slot = slots[i];
                if (slot == null)
                {
                    slots[i] = new InventorySlot(item, amount > item.maxAmount ? item.maxAmount : amount);
                    amount -= item.maxAmount;
                }
                else if (slot.item == item)
                {
                    var prev = slot.amount;
                    slot.amount = amount + slot.amount > item.maxAmount ? item.maxAmount : slot.amount + amount;
                    amount -= item.maxAmount - prev;
                }
                
                if (amount < 0) break;
            }
            
            onAdd.Invoke(item, amount);

            return amount > 0 ? amount : 0;
        }

        private void RemoveItem(int index, int amount)
        {
            if (slots.Length <= index || slots[index] == null) return;
            
            var slot = slots[index];
            
            slot.amount -= amount;
            if (slot.amount < 0) slots[index] = null;
        }

        public void RemoveItem(Item item, int amount)
        {
            foreach (var slot in slots)
            {
                if(slot.item != item) continue;
                
                
            }
        }
    }
}