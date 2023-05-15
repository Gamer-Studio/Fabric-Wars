using System.Collections;
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
        private Inventory inventory => PlayerManager.Instance.currentPlayer.inventory;
        [SerializeField, GetSet("open")] private bool _open = false;
        public bool open
        {
            get => _open;
            set
            {
                _open = value;
                onStateChange.Invoke(value);
            }
        }

        [SerializeField] private RectTransform rect;
        public float openSpeed = 16;

        #endregion variables

        private void Start()
        {
            rect = transform as RectTransform;

            StartCoroutine(Move());
        }

        public void PinClicked() => open = !open;

        private IEnumerator Move()
        {
            while (true)
            {
                var anchor = rect.anchoredPosition;

                if (open) rect.anchoredPosition = anchor.y < 100 ? anchor.Add(0, openSpeed) : anchor.Y(100);
                else rect.anchoredPosition = anchor.y > -100 ? anchor.Add(0, -openSpeed) : anchor.Y(-100);

                yield return new WaitForFixedUpdate();
            }
        }
    }
}