using System;
using System.Collections;
using System.Collections.Generic;
using FabricWars.Game;
using FabricWars.Utils.Attributes;
using FabricWars.Utils.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace FabricWars.Scenes.Game
{
    public class InventoryViewer : MonoBehaviour
    {
        public UnityEvent<bool> onStateChange;

        [SerializeField, GetSet("open")] private bool _open = false;
        public bool open
        {
            get => _open;
            set
            {
                _open = value;
                onStateChange.Invoke(value);
                if (value) SyncInventory(PlayerManager.Instance.currentPlayer.inventory);
            }
        }

        [Header("KeyBinds"), SerializeField] 
        private KeyCode openInventory;

        public float openSpeed = 16;
        private RectTransform _rect;
        [SerializeField] private Inventory currentSyncInventory;
        [SerializeField] private InventoryViewerSlotBar baseBar;
        [SerializeField] private Transform slotContainer;
        [SerializeField] private List<InventoryViewerSlotBar> bars;


        private void Start()
        {
            Settings.keyMappings.TryGetValue("playerManager.openInventory", out openInventory);
            _rect = transform as RectTransform;

            StartCoroutine(ViewerSlide());
        }

        private void Update()
        {
            if (Input.GetKeyDown(openInventory)) open = !open;
        }

        public void PinClicked() => open = !open;

        private IEnumerator ViewerSlide()
        {
            while (true)
            {
                var anchor = _rect.anchoredPosition;

                if (open) _rect.anchoredPosition = anchor.y < 99 ? anchor.Add(0, openSpeed) : anchor.Y(100);
                else _rect.anchoredPosition = anchor.y > -99 ? anchor.Add(0, -openSpeed) : anchor.Y(-100);

                yield return new WaitForFixedUpdate();
            }
        }

        private UnityAction<int> _action;

        public void SyncInventory(Inventory inventory)
        {
            if (inventory == null) return;

            if (currentSyncInventory != inventory)
            {
                if (currentSyncInventory != null && _action != null)
                    currentSyncInventory.OnSlotChanged.RemoveListener(_action);

                // init
                foreach (var bar in bars)
                {
                    Destroy(bar.gameObject);
                }

                var slots = inventory._slots;
                var length = Math.DivRem(slots.Length, 10, out var rem);

                for (var i = 0; i < length + (rem > 0 ? 1 : 0); i++)
                {
                    var bar = Instantiate(baseBar, slotContainer);
                    var barObj = bar.gameObject;

                    if (i == length)
                    {
                        for (var j = 9; j > -1; j--)
                        {
                            if (j > rem - 1) bar.slots[j].SetEnable(false);
                            else if (slots[i * 10 + j] != null)
                            {
                                bar.slots[j].item = slots[i * 10 + j].item;
                            }
                        }
                    }
                    else
                    {
                        for (var j = 0; j < 10; j++)
                        {
                            if (slots[i * 10 + j] != null) bar.slots[j].item = slots[i * 10 + j].item;
                        }
                    }

                    barObj.SetActive(true);
                    bars.Add(bar);
                }

                // subscribe event
                _action = UpdateInventory;
                inventory.OnSlotChanged.AddListener(_action);

                currentSyncInventory = inventory;
            }
        }

        private void UpdateInventory(int index)
        {
            var slotData = currentSyncInventory._slots[index];

            var slotObj = bars[Math.DivRem(index, 10, out var i)].slots[i];
            slotObj.item = slotData.item;
        }
    }
}