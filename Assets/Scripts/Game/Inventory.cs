using System;
using FabricWars.Game.Items;
using UnityEngine;
using UnityEngine.Events;

namespace FabricWars.Game
{
    public class Inventory : MonoBehaviour
    {
        public UnityEvent<int> OnSlotChanged;

        public InventorySlot[] _slots = new InventorySlot[10];

        public int length
        {
            get => _slots.Length;
            set
            {
                if (value <= 0 || value == _slots.Length) return;
                if (value > _slots.Length)
                {
                    Array.Resize(ref _slots, value);
                    return;
                }

                if (value < _slots.Length)
                {
                    
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
            for (var i = 0; i < _slots.Length; i++)
            {
                var slot = _slots[i];
                if (InventorySlot.IsNullOrEmpty(slot))
                {
                    _slots[i] = new InventorySlot(item, amount > item.maxAmount ? item.maxAmount : amount, this, i);
                    amount -= item.maxAmount;
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
                }

                if (amount < 1) break;
            }

            return amount > 0 ? amount : 0;
        }

        private void RemoveItem(int index, int amount)
        {
            if (_slots.Length <= index || _slots[index] == null) return;

            var slot = _slots[index];

            slot.amount -= amount;
            if (slot.amount < 0) _slots[index] = null;
        }

        public void RemoveItem(Item item, int amount)
        {
            for (var i = _slots.Length - 1; i > 0; i--)
            {
                var slot = _slots[i];
                if (slot == null || slot.item != item) continue;

                var prev = slot.amount;
                slot.amount -= amount;
                amount -= prev;

                if (slot.amount < 0) _slots[i] = null;
                if (amount < 0) break;
            }
        }
    }
}