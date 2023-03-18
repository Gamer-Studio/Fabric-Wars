using FabricWars.Game.Elements;
using FabricWars.Graphics;
using FabricWars.Utils;
using FabricWars.Utils.Attributes;
using FabricWars.Utils.Extensions;
using UnityEngine;
using UnityEngine.Serialization;
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
        [FormerlySerializedAs("fillImage")] [SerializeField] private Image backgroundFillImage;
        [SerializeField] private Image backgroundImage;

        // data
        [SerializeField, GetSet("element")] private Element _element = Element.None;

        public Element element
        {
            get => _element;
            set
            {
                if (value != Element.None)
                {
                    image.enabled = true;
                    backgroundImage.enabled = true;
                }

                _element = value;

                if (image && image.material)
                {
                    backgroundImage.color = element.color.A(25 / 255f);
                    backgroundFillImage.color = element.color.A(100 / 255f);
                    image.material.SetColor(Color, value.color.A(100 / 255f));
                }
            }
        }

        [SerializeField, GetSet("elementActive")] private bool _elementActive;

        public bool elementActive
        {
            get => _elementActive;
            set
            {
                if (element == Element.None) return;

                _elementActive = value;
                if (image && image.material)
                {
                    image.material.SetBool("_Active", value);
                }
            }
        }

        public GaugeInt storage = new(0, 100, 100);

        private void Start()
        {
            if (activeShader && image)
            {
                var mat = image.material = new Material(activeShader);

                if (element == Element.None)
                {
                    image.enabled = false;
                    backgroundFillImage.enabled = false;
                    backgroundImage.enabled = false;
                }
                else
                {
                    mat.SetColor(Color, element.color.A(100 / 255f));
                    mat.SetTexture(MainTex, shaderConfig.texture);
                    mat.SetColor(MaskingColor, shaderConfig.maskColor);
                    backgroundImage.color = element.color.A(25 / 255f);
                }
            }
        }
        
        private bool syncStorageValue = false;
        
        [SerializeField, GetSet("activeValue")] private int _activeValue = 0;

        public int activeValue
        {
            get => _activeValue;
            set
            {
                if (!elementActive) return;
                
                _activeValue = value > storage.value ? storage.value : value;
                image.fillAmount = (float)(_activeValue - storage.min) / (storage.max - storage.min);
            }
        }

        public void Init(Element element, GaugeInt storage)
        {
            this.element = element;
            this.storage = storage;
            storage.onChange.AddListener(gauge =>
            {
                backgroundFillImage.fillAmount = gauge.GetFillRatio();

                if (syncStorageValue) activeValue = storage.value;
                else if (activeValue > gauge.value) activeValue = gauge.value;
            });
            backgroundFillImage.fillAmount = storage.GetFillRatio();
        }
        

        public void Activate()
        {
            elementActive = !elementActive;
            image.enabled = elementActive;
            syncStorageValue = elementActive;
        }

        public void Activate(int value)
        {
            if(value < 0) value = 0;
            
            syncStorageValue = false;
            elementActive = true;
            image.enabled = true;
            activeValue = storage.value < value ? storage.value : value;
        }
    }
}