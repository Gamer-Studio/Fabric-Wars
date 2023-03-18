using System.Collections.Generic;
using System.Linq;
using FabricWars.Game.Items;
using FabricWars.Utils.KeyBinds;
using FabricWars.Utils.Serialization;
using UnityEngine;
using UnityEngine.Pool;
using Element = FabricWars.Game.Elements.Element;

namespace FabricWars.Scenes.Board.Attributes
{
    public class ElementManager : MonoBehaviour
    {
        public static ElementManager instance { get; private set; }

#if UNITY_EDITOR
        public SerializableDictionary<Element, ElementSlot> attributes;
#endif
        
        private ObjectPool<ElementSlot> _pool;
        [SerializeField] private GameObject originalSlot;
        [SerializeField] private Transform slotContainer;
        
        public List<ElementSlot> slots = new ();
        public List<ElementSlot> activeSlots = new ();
        
        private void Awake()
        {
            if (instance)
            {
                Destroy(this);
                return;
            }

            _pool = new ObjectPool<ElementSlot>(
                () => Instantiate(originalSlot, slotContainer).GetComponent<ElementSlot>(),
                slots.Add,
                slot => slots.Remove(slot),
                null, false, 1, 10
            );
            
            KeyBindManager.instance
                .Bind(BindOptions.downOnly, KeyCodeUtils.Numberics)
                .Then(obj =>
                {
                    if (!KeyCodeUtils.TryToInt(obj[0], out var val) ||
                        val is 0 or -1 ||
                        val > slots.Count) return;
                    var slot = slots[val - 1];
                    slot.active = !slot.active;
                    if (slot.active) activeSlots.Add(slot);
                    else activeSlots.Remove(slot);
                });

            instance = this;
        }

        public bool TryGetSlot(Element type, out ElementSlot slot)
        {
            slot = slots.FirstOrDefault(attrSlot => attrSlot.type == type);
            
            return slot != null;
        }

        public bool AddSlot(Element attr, int maxValue, out ElementSlot slot)
        {
            slot = null;
            if (
                slots.Count > 8 ||
                attributes.ContainsKey(attr)
            ) return false;

            slot = _pool.Get();
            slot.Init(attr, new (0, maxValue, 0));
            attributes[attr] = slot;

            return true;
        }

        public void SetElementValue(Element element, int value)
        {
            if (TryGetSlot(element, out var slot))
            {
                slot.storage.value = value;
            }
        }
        
        public void AddElementValue(Element element, int value)
        {
            if (TryGetSlot(element, out var slot))
            {
                slot.storage.value += value;
            }
        }
    }
}