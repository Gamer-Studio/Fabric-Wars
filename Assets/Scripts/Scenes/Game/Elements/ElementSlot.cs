using FabricWars.Game.Elements;
using FabricWars.Graphics.Shaders;
using FabricWars.Utils;
using FabricWars.Utils.Attributes;
using FabricWars.Utils.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace FabricWars.Scenes.Game.Elements
{
    public class ElementSlot : MonoBehaviour
    {
        // Property Ids
        private static readonly int Color = Shader.PropertyToID("_MainColor");
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        private static readonly int MaskingColor = Shader.PropertyToID("_MaskingColor");

        [Header("Components")]
        [SerializeField] private MaskingShader shaderConfig;
        [SerializeField] private Image activeFillImage;
        [SerializeField] private Image backgroundFillImage;
        [SerializeField] private Image backgroundImage;

        [Header("Properties")]
        [SerializeField, GetSet("element")] private Element _element = Element.None;

        public Element element
        {
            get => _element;
            set
            {
                if (value != Element.None)
                {
                    activeFillImage.enabled = true;
                    backgroundImage.enabled = true;
                }

                _element = value;

                if (activeFillImage && activeFillImage.material)
                {
                    backgroundImage.color = element.color.A(25 / 255f);
                    backgroundFillImage.color = element.color.A(100 / 255f);
                    activeFillImage.material.SetColor(Color, value.color.A(100 / 255f));
                }
            }
        }

        [SerializeField, GetSet("elementActive")]
        private bool _elementActive;

        public bool elementActive
        {
            get => _elementActive;
            set
            {
                if (element == Element.None) return;

                _elementActive = value;
                activeFillImage.enabled = value;
                activeFillImage.fillAmount = backgroundFillImage.fillAmount;
            }
        }

        public GaugeInt storage = new(0, 100, 0);

        private void Start()
        {
            if (activeFillImage)
            {
                var mat = activeFillImage.material = new Material(activeFillImage.material.shader);

                if (element == Element.None)
                {
                    activeFillImage.enabled = false;
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

        private bool _storageValueSync = false;

        [SerializeField, GetSet("activeValue")]
        private int _activeValue = 0;

        public int activeValue
        {
            get => _activeValue;
            set
            {
                if (!elementActive || !_storageValueSync) return;

                _activeValue = value > storage.value ? storage.value : value;
                activeFillImage.fillAmount = (float)(_activeValue - storage.min) / (storage.max - storage.min);
            }
        }

        public void Init(Element element, GaugeInt storage)
        {
            this.element = element;
            this.storage = storage;
            storage.onChange.AddListener(gauge =>
            {
                backgroundFillImage.fillAmount = gauge.GetFillRatio();

                if (_storageValueSync) activeValue = storage.value;
                else if (activeValue > gauge.value) activeValue = gauge.value;
            });
            backgroundFillImage.fillAmount = storage.GetFillRatio();
            activeFillImage.fillAmount = 0;
        }


        public void Activate()
        {
            elementActive = !elementActive;
            _storageValueSync = elementActive;
            activeValue = storage.value;
        }

        public void Activate(int value)
        {
            if (value < 0) value = 0;

            _storageValueSync = false;
            elementActive = true;
            activeValue = storage.value < value ? storage.value : value;
        }
    }
}