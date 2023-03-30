using FabricWars.Game.Entities.Core;
using UnityEngine;

namespace FabricWars.Game.Items
{
    public class ItemPointerController : MonoBehaviour
    {
        [SerializeField] private GameObject _bumped;

        private void Update()
        {
            if (Input.GetMouseButtonUp(0) && _bumped != null)
            {
                if (ItemObject._bumpedItems != null)
                {
                    var objs = ItemObject._bumpedItems.ConvertAll(obj => obj);
                    _bumped.SendMessage("InjectItems", objs, SendMessageOptions.DontRequireReceiver);
                }

                if (_bumped.TryGetComponent<Transducer>(out var trans)) trans.Hover(false);
                
                _bumped = null;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Entity")) _bumped = col.gameObject;

            if (col.gameObject.TryGetComponent<Transducer>(out var trans)) trans.Hover(true);
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject == _bumped) _bumped = null;
            if (col.gameObject.CompareTag("Entity") && col.gameObject.TryGetComponent<Transducer>(out var trans))
                trans.Hover(false);
        }
    }
}