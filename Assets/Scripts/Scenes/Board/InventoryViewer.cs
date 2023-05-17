using System.Collections;
using System.Collections.Generic;
using FabricWars.Game;
using FabricWars.Utils.Attributes;
using FabricWars.Utils.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace FabricWars.Scenes.Board
{
    public class InventoryViewer : MonoBehaviour
    {
        #region events

        public UnityEvent<bool> onStateChange;

        #endregion events

        #region variables
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

        public float openSpeed = 16;
        private RectTransform _rect;
        [SerializeField] private Inventory currentSyncInventory;
        [SerializeField] private InventoryViewerSlotBar baseBar;
        [SerializeField] private Transform slotContainer;
        [SerializeField] private List<InventoryViewerSlotBar> bars;
        #endregion variables

        private void Start()
        {
            _rect = transform as RectTransform;

            StartCoroutine(Move());
        }

        public void PinClicked() => open = !open;

        private IEnumerator Move()
        {
            while (true)
            {
                var anchor = _rect.anchoredPosition;

                if (open) _rect.anchoredPosition = anchor.y < 100 ? anchor.Add(0, openSpeed) : anchor.Y(100);
                else _rect.anchoredPosition = anchor.y > -100 ? anchor.Add(0, -openSpeed) : anchor.Y(-100);

                yield return new WaitForFixedUpdate();
            }
        }

        public void SyncInventory(Inventory inventory)
        {
            if (currentSyncInventory != inventory)
            {
                
            }
            
            currentSyncInventory = inventory;
        }
    }
}