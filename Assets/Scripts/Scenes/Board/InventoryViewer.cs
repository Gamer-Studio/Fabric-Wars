using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace FabricWars.Scenes.Board
{
    public class InventoryViewer : MonoBehaviour
    {
        public UnityEvent<bool> onStateChange;

        private bool _open = false;
        public bool open
        {
            get => _open;
            set
            {
                _open = value;
                onStateChange.Invoke(value);

                if (_openCoroutine != null) StopCoroutine(_openCoroutine);
                StartCoroutine(Open(value));
            }
        }

        private Coroutine _openCoroutine;
        private IEnumerator Open(bool hide = false)
        {
            Debug.Log(hide);
            yield break;
        }
    }
}