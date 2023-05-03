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
        private static Camera mainCamera => ElementManager.instance.mainCamera;
        private static Tilemap tilemap => ElementManager.instance.tilemap;

        public Item type
        {
            get => _type;
            set
            {
                name = $"Item_{value.name}";
                _type = value;
                if (spriteRenderer) spriteRenderer.sprite = value.sprite;

                var physics = new List<Vector2>();
                value.sprite.GetPhysicsShape(0, physics);
                collider.SetPath(0, physics);
            }
        }

        private static Coroutine _dragFunc;
        public static ItemObject dragObj { get; private set; }
        private static ItemPointerController _pointer;

        private static ItemPointerController pointer
        {
            get
            {
                if (_pointer != null) return _pointer;
                else
                {
                    var obj = new GameObject
                    {
                        name = "ItemObject_Pointer", transform = { position = Vector3.zero }, tag = "GameController",
                        layer = 5
                    };
                    var col = obj.AddComponent<CircleCollider2D>();
                    obj.AddComponent<Rigidbody2D>();
                    col.radius = 0.1f;
                    col.isTrigger = true;
                    _pointer = obj.AddComponent<ItemPointerController>();

                    return _pointer;
                }
            }
        }

        private void OnClick(bool inputValue)
        {
            if (inputValue)
            {
                pointer.transform.position =
                    BoardManager.instance.mainCamera.ScreenToWorldPoint(Input.mousePosition).Z(0);
                _dragFunc = StartCoroutine(StartDrag());
            }
            else
            {
                var transducer = Transducer.defaultInstance;
                var mPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var targetPos = tilemap.ToTilemapPosition(mPos);

                if (transducer != null)
                {
                    foreach (var obj in _bumpedItems)
                    {
                        if (!tilemap.HasTile(obj.transform.position))
                        {
                            obj.transform.position = transducer.transform.position.Z(obj.transform.position.z);
                        }
                    }

                    if (!tilemap.HasTile(targetPos))
                    {
                        var pos = transducer.transform.position;
                        transform.position = transform.position.XY(pos.x, pos.y);
                    }
                }
            }
        }


        private void OnCollisionEnter2D(Collision2D col)
        {
            if (this != dragObj || !col.gameObject.CompareTag("Item")) return;

            var comp = col.gameObject.GetComponent<ItemObject>();
            if (!_bumpedItems.Contains(comp) && comp.type == type) _bumpedItems.Add(comp);
        }

        public static readonly List<ItemObject> _bumpedItems = new();

        private IEnumerator StartDrag()
        {
            if (!type.canCatch) yield break;

            _bumpedItems.Clear();
            dragObj = this;
            _bumpedItems.Add(this);
            while (true)
            {
                var targetPos = BoardManager.instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var prevPos = pointer.transform.position;
                var distance = prevPos.Vector3Distance(targetPos);

                prevPos = prevPos.XY(targetPos.x, targetPos.y);
                pointer.transform.position = prevPos;

                transform.position = transform.position.XY(targetPos.x, targetPos.y);
                foreach (var obj in _bumpedItems)
                {
                    obj.transform.position = obj.transform.position.Add(distance.x, distance.y, 0);
                }

                yield return new WaitForFixedUpdate();
            }
        }

        public static void StopDrag()
        {
            if (_dragFunc == null || !dragObj.gameObject) return;

            dragObj.StopCoroutine(_dragFunc);
            dragObj = null;
            _dragFunc = null;
        }
    }
}