using FabricWars.Game.Items;
using FabricWars.Graphics;
using FabricWars.Utils.Attributes;
using FabricWars.Utils.Overrides;
using UnityEngine;
using UnityEngine.UI;

namespace FabricWars.Scenes.Board.Attributes
{
    public class ItemAttributeSlot : MonoBehaviour
    {
        private static readonly int Color = Shader.PropertyToID("_MainColor");
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        private static readonly int MaskingColor = Shader.PropertyToID("_MaskingColor");

        public ItemAttribute type = ItemAttribute.None;
        [SerializeField] private Toggle toggle;
        [SerializeField] private Shader activeShader;
        [SerializeField] private MaskingShader shaderConfig;
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
                mat.SetTexture(MainTex, shaderConfig.texture);
                mat.SetColor(MaskingColor, shaderConfig.maskColor);
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