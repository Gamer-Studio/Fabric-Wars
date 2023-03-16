using System.Collections.Generic;
using FabricWars.Game.Items;
using FabricWars.Utils;
using FabricWars.Utils.KeyBinds;
using FabricWars.Utils.Serialization;
using UnityEngine;
using UnityEngine.Pool;

namespace FabricWars.Scenes.Board.Attributes
{
    public class ItemAttributeManager : MonoBehaviour
    {
        public static ItemAttributeManager instance { get; private set; }

        public SerializableDictionary<ItemAttribute, ItemAttributeSlot> attributeInventory;
        
        private ObjectPool<ItemAttributeSlot> _pool;
        [SerializeField] private GameObject originalSlot;
        [SerializeField] private Transform slotContainer;
        [SerializeField] private List<ItemAttributeSlot> slots;

        private void Awake()
        {
            if (instance)
            {
                Destroy(this);
                return;
            }

            _pool = new ObjectPool<ItemAttributeSlot>(
                () => Instantiate(originalSlot, slotContainer).GetComponent<ItemAttributeSlot>(),
                slot =>
                {
                    slots.Add(slot);
                    slot.name = $"AttributeSlot_{slots.Count}";
                },
                slot =>
                {
                    slots.Remove(slot);
                },
                null, false, 1, 20
            );
            
            KeyBindManager.instance
                .Bind(BindOptions.downOnly, KeyCode.Alpha1)
                .Then(() =>
                {
                    Debug.Log("1");
                });
            
            instance = this;
        }

        public bool TryGetSlot(ItemAttribute attr, out ItemAttributeSlot slot) =>
            attributeInventory.TryGetValue(attr, out slot);
        
        
    }
}