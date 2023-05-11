using System;
using FabricWars.Game.Items;
using UnityEngine;
using UnityEngine.Events;

namespace FabricWars.Game
{
    public class Inventory : MonoBehaviour
    {
        public UnityEvent<int> OnSlotChanged;

        public InventorySlot[] slots = new InventorySlot[10];

        public int length
        {
            get => slots.Length;
            set
            {
                switch (value)
                {
                    case var _ when value <= 0 || value == slots.Length: return;
                    case var _ when value > slots.Length:
                        Array.Resize(ref slots, value);
                        return;
                    case var _ when value < slots.Length:
                    {
                        return;
                    }
                }
            }
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
                if (InventorySlot.IsNullOrEmpty(slot))
                {
                    slots[i] = new InventorySlot(item, amount > item.maxAmount ? item.maxAmount : amount);
                    amount -= item.maxAmount;

                    OnSlotChanged.Invoke(i);
                }
                else if (slot.item == item)
                {
                    if (amount + slot.amount > item.maxAmount)
                    {
                        amount -= item.maxAmount - slot.amount;
                        slot.amount = item.maxAmount;
                    }
                    else
                    {
                        slot.amount += amount;
                        amount = 0;
                    }

                    OnSlotChanged.Invoke(i);
                }

                if (amount < 1) break;
            }

            return amount > 0 ? amount : 0;
        }

        private void RemoveItem(int index, int amount)
        {
            if (slots.Length <= index || slots[index] == null) return;

            var slot = slots[index];

            slot.amount -= amount;
            if (slot.amount < 0) slots[index] = null;
            OnSlotChanged.Invoke(index);
        }

        public void RemoveItem(Item item, int amount)
        {
            for (var i = slots.Length - 1; i > 0; i--)
            {
                var slot = slots[i];
                if (slot == null || slot.item != item) continue;

                var prev = slot.amount;
                slot.amount -= amount;
                amount -= prev;

                if (slot.amount < 0) slots[i] = null;
                if (amount < 0) break;
            }
        }
    }
}