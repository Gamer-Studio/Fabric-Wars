using System;
using FabricWars.Game.Elements;
using FabricWars.Utils.Attributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

namespace FabricWars.Game.Entities
{
    public class MonsterNest : Entity
    {
        [SerializeField] private Light2D elementMark;
        [SerializeField, GetSet("element")] private Element _element = Element.None;
        public Element element
        {
            get => _element;
            set
            {
                _element = value;
                if (value != null)
                {
                    if (elementMark != null) elementMark.lightCookieSprite = element.mark;
                }
            }
        }

        public UnityEvent<int> OnLevelChanged;
        [SerializeField, GetSet("level")] private int _level;
        public int level
        {
            get => _level;
            set
            {
                _level = value;
                OnLevelChanged.Invoke(value);
            }
        }

        public UnityEvent<float> OnResize;
        [SerializeField, GetSet("size")] private float _size;
        public float size
        {
            get => _size;
            set
            {
                _size = value;
                OnResize.Invoke(value);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(collision.gameObject.name);
        }
    }
}