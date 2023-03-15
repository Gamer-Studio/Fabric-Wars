﻿using FabricWars.Game.Items;
using FabricWars.Graphics;
using FabricWars.Utils.Attributes;
using FabricWars.Utils.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace FabricWars.Scenes.Board.Attributes
{
    public class ItemAttributeSlot : MonoBehaviour
    {
        // Property Ids
        private static readonly int Color = Shader.PropertyToID("_MainColor");
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        private static readonly int MaskingColor = Shader.PropertyToID("_MaskingColor");

        // components
        [SerializeField] private Toggle toggle;
        [SerializeField] private Shader activeShader;
        [SerializeField] private MaskingShader shaderConfig;
        [SerializeField] private Image image;
        [SerializeField] private Image backgroundImage;

        // data
        [SerializeField, GetSet("type")] private ItemAttribute _type = ItemAttribute.None;

        public ItemAttribute type
        {
            get => _type;
            set
            {
                if (value != ItemAttribute.None)
                {
                    image.enabled = true;
                    backgroundImage.enabled = true;
                }

                _type = value;
                
                if (image && image.material)
                {
                    backgroundImage.color = type.GetColor().A(25 / 255f);
                    image.material.SetColor(Color, value.GetColor());
                }
            }
        }

        [SerializeField, GetSet("active")] private bool _active;

        public bool active
        {
            get => _active;
            set
            {
                if (type == ItemAttribute.None) return;

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

                if (type == ItemAttribute.None)
                {
                    image.enabled = false;
                    backgroundImage.enabled = false;
                }
                else
                {
                    mat.SetColor(Color, type.GetColor());
                    mat.SetTexture(MainTex, shaderConfig.texture);
                    mat.SetColor(MaskingColor, shaderConfig.maskColor);
                    backgroundImage.color = type.GetColor().A(25 / 255f);
                }
            }

            if (toggle != null) toggle.onValueChanged.AddListener(val => active = val);
        }
    }
}