using System;
using FabricWars.Game.Items;
using UnityEngine;
using UnityEngine.Events;

namespace FabricWars.Game
{
    public class Inventory : MonoBehaviour
    {
        public UnityEvent<int> OnSlotChanged;
        public UnityEvent<InventorySlot> OnSlotRemoved;

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
                    for (var i = value; i < _slots.Length; i++)
                    {
                        var slot = _slots[i];
                        if (slot != null) OnSlotRemoved.Invoke(slot);
                        _slots[i] = null;
                    }

                    Array.Resize(ref _slots, value);
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
            // Check existing slots for the same item
            foreach (var slot in _slots)
            {
                if (slot != null && slot.item == item)
                {
                    var addAmount = Math.Min(amount, item.maxAmount - slot.amount);
                    slot.amount += addAmount;
                    amount -= addAmount;
                }

                if (amount <= 0) break;
            }

            // If there are still items to add, check for empty slots
            if (amount > 0)
            {
                for (var i = 0; i < _slots.Length; i++)
                {
                    if (InventorySlot.IsNullOrEmpty(_slots[i]))
                    {
                        var addAmount = Math.Min(amount, item.maxAmount);
                        _slots[i] = new InventorySlot(item, addAmount, this, i);
                        OnSlotChanged.Invoke(i);
                        amount -= addAmount;
                    }

                    if (amount <= 0) break;
                }
            }

            // Return the amount of items that could not be added
            return amount;
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