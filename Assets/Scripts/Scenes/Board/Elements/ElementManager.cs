using System.Collections.Generic;
using System.Linq;
using FabricWars.Game.Elements;
using FabricWars.Utils.KeyBinds;
using UnityEngine;
using UnityEngine.Pool;

namespace FabricWars.Scenes.Board.Elements
{
    public class ElementManager : MonoBehaviour
    {
        public static ElementManager instance { get; private set; }
        
        private ObjectPool<ElementSlot> _pool;
        [SerializeField] private GameObject originalSlot;
        [SerializeField] private Transform slotContainer;
        
        public List<ElementSlot> slots = new ();
        [SerializeField] private List<ElementSlot> activeSlots = new ();
        
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
                slot =>
                {
                    activeSlots.Remove(slot);
                    slots.Remove(slot);
                },
                null, false, 1, 10
            );
            
            KeyBindManager.instance
                .Bind(BindOptions.downOnly, KeyCodeUtils.Numberics)
                .Then(obj =>
                {
                    if (EVIDialogue.isActiveAndEnabled) return;

                    if (!KeyCodeUtils.TryToInt(obj[0], out var val) ||
                        val is 0 or -1 ||
                        val > slots.Count) return;
                    
                    var slot = slots[val - 1];
                    
                    if (Input.GetKey(KeyCode.LeftShift)) slot.Activate(GetElementInputValue(slot));
                    else slot.Activate();
                    
                    if (slot.elementActive) activeSlots.Add(slot);
                    else activeSlots.Remove(slot);
                });

            instance = this;
        }

        public bool TryGetSlot(Element type, out ElementSlot slot)
        {
            slot = slots.FirstOrDefault(attrSlot => attrSlot.element == type);
            
            return slot != null;
        }

        public bool AddSlot(Element element, int maxValue, out ElementSlot slot)
        {
            slot = null;
            if (slots.Count > 8 || IsSlotExist(element)) return false;
            
            slot = _pool.Get();
            slot.Init(element, new (0, maxValue, 0));

            return true;
        }

        public void RemoveSlot(Element element)
        {
            if (slots.Count == 0) return;
            
            if (!IsSlotExist(element)) return;

            var slot = slots.FirstOrDefault(attrSlot => attrSlot.element == element);
            
            _pool.Release(slot);
        }

        public bool IsSlotExist(Element element)
        {
            return slots.Any(attrSlot => attrSlot.element == element);
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
            else
            {
                AddSlot(element, 10, out slot);
                slot.storage.value = value;
            }
        }

        // TODO: 이걸로 유저가 설정한 원소(Element) 수량 가져와서  ElementSlot에 해당 값만큼 설정해서 활성화하게 할 생각
        [SerializeField] private ElementValueInputDialogue EVIDialogue;
        private int GetElementInputValue(ElementSlot slot)
        {
            return 0;
        }

        public IEnumerable<(Element element, int value)> GetActiveElements()
        {
            return from slot in activeSlots
                select (slot.element, slot.activeValue);
        }
    }
}