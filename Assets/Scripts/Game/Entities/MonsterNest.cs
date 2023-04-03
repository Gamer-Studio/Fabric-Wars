using FabricWars.Game.Elements;
using FabricWars.Utils.Attributes;
using UnityEngine;
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
    }
}