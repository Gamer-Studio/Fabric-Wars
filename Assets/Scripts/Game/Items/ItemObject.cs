using System.Collections;
using System.Collections.Generic;
using FabricWars.Game.Entities.Core;
using FabricWars.Scenes.Board;
using FabricWars.Scenes.Board.Elements;
using FabricWars.Utils.Attributes;
using FabricWars.Utils.Extensions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FabricWars.Game.Items
{
    public class ItemObject : MonoBehaviour
    {
        [SerializeField, GetSet("type")] private Item _type;
        [Header("Components")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private new PolygonCollider2D collider;
        private Camera mainCamera => ElementManager.instance.mainCamera;
        private Tilemap tilemap => ElementManager.instance.tilemap;
        
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
                var transducer = Transducer.defaultInstance;
                var mPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var targetPos = new Vector3Int(
                    (int)mPos.x - (mPos.x < 0 ? 1 : 0),
                    (int)mPos.y - (mPos.y < 0 ? 1 : 0)
                );

                if (transducer != null)
                {
                    if (tilemap.GetTile(targetPos) == null)
                    {
                        var pos = transducer.transform.position;
                        transform.position = transform.position.XY(pos.x, pos.y);
                    }
                
                    if (transducer.bumpedObject == gameObject)
                    {
                        transducer.ConsumeItem(this);
                    }
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