using FabricWars.Game.Elements;
using FabricWars.Graphics;
using FabricWars.Utils;
using FabricWars.Utils.Attributes;
using FabricWars.Utils.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace FabricWars.Scenes.Board.Elements
{
    public class ElementSlot : MonoBehaviour
    {
        // Property Ids
        private static readonly int Color = Shader.PropertyToID("_MainColor");
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        private static readonly int MaskingColor = Shader.PropertyToID("_MaskingColor");

        // components
        [SerializeField] private Shader activeShader;
        [SerializeField] private MaskingShader shaderConfig;
        [SerializeField] private Image image;
        [SerializeField] private Image fillImage;
        [SerializeField] private Image backgroundImage;

        // data
        [SerializeField, GetSet("type")] private Element _type = Element.None;

        public Element type
        {
            get => _type;
            set
            {
                if (value != Element.None)
                {
                    image.enabled = true;
                    backgroundImage.enabled = true;
                }

                _type = value;

                if (image && image.material)
                {
                    backgroundImage.color = type.color.A(25 / 255f);
                    fillImage.color = type.color.A(100 / 255f);
                    image.material.SetColor(Color, value.color.A(100 / 255f));
                }
            }
        }

        [SerializeField, GetSet("active")] private bool _active;

        public bool active
        {
            get => _active;
            set
            {
                if (type == Element.None) return;

                _active = value;
                if (image && image.material)
                {
                    image.material.SetBool("_Active", value);
                }
            }
        }

        public GaugeInt storage = new (0, 100, 100);

        private void Start()
        {
            if (activeShader && image)
            {
                var mat = image.material = new Material(activeShader);

                if (type == Element.None)
                {
                    image.enabled = false;
                    fillImage.enabled = false;
                    backgroundImage.enabled = false;
                }
                else
                {
                    mat.SetColor(Color, type.color.A(100 / 255f));
                    mat.SetTexture(MainTex, shaderConfig.texture);
                    mat.SetColor(MaskingColor, shaderConfig.maskColor);
                    backgroundImage.color = type.color.A(25 / 255f);
                }
            }
        }

        public void Init(Element type, GaugeInt storage)
        {
            this.type = type;
            this.storage = storage;
            storage.onChange.AddListener(gauge => fillImage.fillAmount = gauge.GetFillRatio());
            fillImage.fillAmount = storage.GetFillRatio();
        }

        public void Activate()
        {
        }

        public void Activate(int value)
        {
            
        }
    }
}