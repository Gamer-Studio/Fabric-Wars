using System.Collections;
using System.Collections.Generic;
using FabricWars.Game.Entities.Core;
using FabricWars.Scenes.Board;
using FabricWars.Utils.Attributes;
using UnityEngine;

namespace FabricWars.Game.Items
{
    public class ItemObject : MonoBehaviour
    {
        [SerializeField, GetSet("type")] private Item _type;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private new PolygonCollider2D collider;
        
        public Item type
        {
            get => _type;
            set
            {
                name = $"Item_{value.name}";
                _type = value;
                if(spriteRenderer) spriteRenderer.sprite = value.sprite;

                var physics = new List<Vector2>();
                value.sprite.GetPhysicsShape(0, physics);
                collider.SetPath(0, physics);
            }
        }

        private static Coroutine _dragFunc;
        public static ItemObject dragObj { get; private set; }

        private void OnClick(bool inputValue)
        {
            if (inputValue)
            {
                _dragFunc = StartCoroutine(StartDrag());
            }
            else
            {
                if (Transducer.defaultInstance && Transducer.defaultInstance.bumpedObject == gameObject)
                {
                    Transducer.defaultInstance.ConsumeItem(this);
                }
            }
        }

        private IEnumerator StartDrag()
        {
            if (!type.canCatch) yield break;
            
            dragObj = this;
            while (true)
            {
                var targetPos = BoardManager.instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
            
                yield return new WaitForFixedUpdate();
            }
        }

        public static void StopDrag()
        {
            if(_dragFunc == null || !dragObj.gameObject) return;
            
            dragObj.StopCoroutine(_dragFunc);
            dragObj = null;
            _dragFunc = null;
        }
    }
}