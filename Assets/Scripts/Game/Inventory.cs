using System;
using FabricWars.Game.Items;
using FabricWars.Utils.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace FabricWars.Game
{
    public class Inventory : MonoBehaviour
    {
        public UnityEvent<SerializablePair<Item, int>> onDrop;
        
        [SerializeField] private InventorySlot[] _slots = new InventorySlot[10];

        public int length
        {
            get => _slots.Length;
            set
            {
                switch (value)
                {
                    case var _ when value <= 0 || value == _slots.Length: return;
                    case var _ when value > _slots.Length: Array.Resize(ref _slots, value); return;
                    case var _ when value < _slots.Length:
                    {
                        return;
                    }
                }
            }
        }

        private void Awake()
        {
            
        }

        private void DropItem(int index)
        {
        }
    }
}