using System.Collections;
using FabricWars.Game.Items;
using FabricWars.Scenes.Board;
using FabricWars.Scenes.Board.Elements;
using FabricWars.Utils.Attributes;
using FabricWars.Utils.Extensions;
using UnityEngine;
using Element = FabricWars.Game.Elements.Element;

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
            if(!defaultInstance) defaultInstance = this;
        }

        protected override void Start()
        {
            base.Start();
            
            ElementManager.instance.AddSlot(Element.Causality, 10, out _);
        }

        private Coroutine _resizeRoutine;
        
        private void Hover(bool inputValue)
        {
            if (_resizeRoutine != null)
            {
                StopCoroutine(_resizeRoutine);
                _resizeRoutine = null;
            }
            
            _resizeRoutine = StartCoroutine(ResizeTransducer(inputValue));
        }

        public GameObject bumpedObject { get; private set; }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!ItemObject.dragObj) return;
            
            if (col.CompareTag("Item") && ItemObject.dragObj.gameObject == col.gameObject)
            {
                if (_resizeRoutine != null)
                {
                    StopCoroutine(_resizeRoutine);
                    _resizeRoutine = null;
                }

                bumpedObject = col.gameObject;
                _resizeRoutine = StartCoroutine(ResizeTransducer(true));
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (ItemObject.dragObj == null || (other.CompareTag("Item") && ItemObject.dragObj.gameObject == other.gameObject))
            {
                if (_resizeRoutine != null)
                {
                    StopCoroutine(_resizeRoutine);
                    _resizeRoutine = null;
                }

                bumpedObject = null;
                _resizeRoutine = StartCoroutine(ResizeTransducer(false));
            }
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
            if (!ItemManager.instance) return;
            
            ItemManager.instance.Release(obj);

            if (!ElementManager.instance) return;
            
            foreach (var (element, value) in obj.type.elements)
            {
                ElementManager.instance.AddElementValue(element, value);
            }
        }
    }
}