using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FabricWars.Game.Items;
using FabricWars.Utils.KeyBinds;
using FabricWars.Utils.Serialization;
using UnityEngine;
using UnityEngine.Pool;

namespace FabricWars.Scenes.Board.Attributes
{
    public class ItemAttributeManager : MonoBehaviour
    {
        public static ItemAttributeManager instance { get; private set; }

#if UNITY_EDITOR
        public SerializableDictionary<ItemAttribute, ItemAttributeSlot> attributes;
#endif
        
        private ObjectPool<ItemAttributeSlot> _pool;
        [SerializeField] private GameObject originalSlot;
        [SerializeField] private Transform slotContainer;
        public List<ItemAttributeSlot> slots;

        private void Awake()
        {
            if (instance)
            {
                Destroy(this);
                return;
            }

            slots = new List<ItemAttributeSlot>();

            _pool = new ObjectPool<ItemAttributeSlot>(
                () => Instantiate(originalSlot, slotContainer).GetComponent<ItemAttributeSlot>(),
                slots.Add,
                slot => slots.Remove(slot),
                null, false, 1, 10
            );
            
            KeyBindManager.instance
                .Bind(BindOptions.downOnly, KeyCodeUtils.Numberics)
                .Then(obj =>
                {
                    if (KeyCodeUtils.TryToInt(obj[0], out var val) && 
                        val != 0 && val != -1 &&
                        val <= slots.Count)
                    {
                        var slot = slots[val];
                        slot.active = !slot.active;
                    }
                });

            instance = this;
        }

        public bool TryGetSlot(ItemAttribute type, out ItemAttributeSlot slot)
        {
            slot = slots.FirstOrDefault(attrSlot => attrSlot.type == type);
            
            return slot != null;
        }

        public bool AddSlot(ItemAttribute attr, int maxValue, out ItemAttributeSlot slot)
        {
            slot = null;
            if (
                slots.Count > 8 ||
                attributes.ContainsKey(attr)
            ) return false;
            

            slot = _pool.Get();
            slot.type = attr;
            slot.storage.max = maxValue;
            slot.storage.value = 0;
            attributes[attr] = slot;

            return true;
        }

        [ContextMenu("Test")]
        public void Test()
        {
            AddSlot(ItemAttribute.Fire, 1000, out _);
        }
    }
}