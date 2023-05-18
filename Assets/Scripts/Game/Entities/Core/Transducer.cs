using System.Collections;
using System.Collections.Generic;
using FabricWars.Game.Elements;
using FabricWars.Game.Items;
using FabricWars.Scenes.Game;
using FabricWars.Scenes.Game.Elements;
using FabricWars.Utils.Attributes;
using FabricWars.Utils.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FabricWars.Game.Entities.Core
{
    public class Transducer : Entity
    {
        public static Transducer defaultInstance;

        [SerializeField] private PolygonCollider2D _collider;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField, GetSet("sprite")] private Sprite _sprite;

        public Sprite sprite
        {
            get => _sprite;
            set
            {
                _sprite = value;
                if (value.GetPhysicsShapeCount() > 0 && _collider)
                {
                    _collider.pathCount = value.GetPhysicsShapeCount();

                    for (int i = 0, m = value.GetPhysicsShapeCount(); i < m; i++)
                    {
                        _collider.SetPath(i, value.GetPhysicsShape(i));
                    }
                }

                if (_renderer) _renderer.sprite = value;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            if (!defaultInstance) defaultInstance = this;
        }

        protected override void Start()
        {
            base.Start();

            ElementManager.instance.AddSlot(Element.Karma, 10, out _);
        }

        private Coroutine _resizeRoutine;

        // Unity message event
        public void Hover(bool inputValue)
        {
            if (_resizeRoutine != null)
            {
                StopCoroutine(_resizeRoutine);
                _resizeRoutine = null;
            }

            _resizeRoutine = StartCoroutine(ResizeTransducer(inputValue));
        }

        [SerializeField] private int minSize = 1;
        [SerializeField] private int maxSize = 3;
        [SerializeField] private float resizeSpeed = 1;

        private IEnumerator ResizeTransducer(bool dir)
        {
            while (true)
            {
                if ((dir && transform.localScale.x <= maxSize) || (!dir && transform.localScale.x >= minSize))
                {
                    var scale = transform.localScale;
                    var resizeVal = (dir ? Time.deltaTime : -Time.deltaTime) * resizeSpeed;

                    transform.localScale = new Vector3(scale.x + resizeVal, scale.y + resizeVal, scale.z);

                    yield return new WaitForFixedUpdate();
                }
                else
                {
                    var resizeVal = dir ? maxSize : minSize;
                    transform.localScale = new Vector3(resizeVal, resizeVal, transform.localScale.z);
                    yield break;
                }
            }
        }

        public void ConsumeItem(ItemObject obj)
        {
            if (!ElementManager.instance && !ItemManager.instance) return;

            foreach (var (element, value) in obj.type.elements)
            {
                var eValue = Random.Range(0, value + 1);
                ElementManager.instance.AddElementValue(element, eValue);
            }

            ElementManager.instance.AddElementValue(Element.Karma, 1);

            ItemManager.instance.Release(obj);
        }

        // Unity message event
        private void InjectItems(List<ItemObject> items)
        {
            foreach (var item in items)
            {
                ConsumeItem(item);
            }
        }
    }
}