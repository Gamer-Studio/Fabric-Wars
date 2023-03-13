using FabricWars.Game.Items;
using FabricWars.Utils.Attributes;
using FabricWars.Utils.Overrides;
using UnityEngine;
using UnityEngine.UI;

namespace FabricWars.Scenes.Board.Attributes
{
    public class ItemAttributeSlot : MonoBehaviour
    {
        private static readonly int Color = Shader.PropertyToID("_MainColor");

        public ItemAttribute type = ItemAttribute.None;
        [SerializeField] private Toggle toggle;
        [SerializeField] private Shader activeShader;
        [SerializeField] private Image image;
        [SerializeField, GetSet("active")] private bool _active;

        public bool active
        {
            get => _active;
            set
            {
                _active = value;
                if (image && image.material)
                {
                    image.material.SetBool("_Active", value);
                }
            }
        }

        private void Awake()
        {
            if (activeShader && image)
            {
                var mat = image.material = new Material(activeShader);
                mat.SetColor(Color, type.GetColor());
            }

            if (toggle != null) toggle.onValueChanged.AddListener(val =>
            {
                if (type != ItemAttribute.None)
                {
                    active = val;
                }
            });
        }
    }
}