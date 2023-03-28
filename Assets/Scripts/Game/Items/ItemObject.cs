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

        [Header("Components")] [SerializeField]
        private SpriteRenderer spriteRenderer;

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
                if (spriteRenderer) spriteRenderer.sprite = value.sprite;

                var physics = new List<Vector2>();
                value.sprite.GetPhysicsShape(0, physics);
                collider.SetPath(0, physics);
            }
        }

        private static Coroutine _dragFunc;
        public static ItemObject dragObj { get; private set; }
        private static GameObject _pointer;

        private GameObject pointer
        {
            get => _pointer != null
                ? _pointer
                : _pointer = new GameObject { name = "ItemObject_Pointer", transform = { position = Vector3.zero } };
        }

        private void OnClick(bool inputValue)
        {
            if (inputValue)
            {
                pointer.transform.position = BoardManager.instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                _dragFunc = StartCoroutine(StartDrag());
            }
            else
            {
                var transducer = Transducer.defaultInstance;
                var mPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var targetPos = tilemap.ToTilemapPosition(mPos);

                if (transducer != null)
                {
                    foreach (var obj in _bumped)
                    {
                        if (tilemap.HasTile(obj.transform.position))
                        {
                            obj.transform.position = transducer.transform.position.Z(obj.transform.position.z);
                        }
                    }

                    if (tilemap.HasTile(targetPos))
                    {
                        var pos = transducer.transform.position;
                        transform.position = transform.position.XY(pos.x, pos.y);
                    }

                    if (transducer.bumpedObject == gameObject)
                    {
                        transducer.ConsumeItem(this);
                        foreach (var obj in _bumped)
                        {
                            transducer.ConsumeItem(obj);
                        }
                    }
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (this != dragObj) return;

            if (col.gameObject.CompareTag("Item"))
            {
                var comp = col.gameObject.GetComponent<ItemObject>();
                if (!_bumped.Contains(comp) && comp.type == type) _bumped.Add(comp);
            }
        }

        public static readonly List<ItemObject> _bumped = new();

        private IEnumerator StartDrag()
        {
            if (!type.canCatch) yield break;

            _bumped.Clear();
            dragObj = this;
            while (true)
            {
                var targetPos = BoardManager.instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var prevPos = pointer.transform.position;
                var distance = prevPos.Vector3Distance(targetPos);

                prevPos = prevPos.XY(targetPos.x, targetPos.y);
                pointer.transform.position = prevPos;

                transform.position = transform.position.XY(targetPos.x, targetPos.y);
                foreach (var obj in _bumped)
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