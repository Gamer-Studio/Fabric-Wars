using System;
using FabricWars.Utils.Attributes;
using UnityEngine;
using UnityEngine.Localization;

namespace FabricWars.Graphics.W2D
{
    [AddComponentMenu("W2D/W2D Text")]
    public class W2DText : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField, GetSet("text")] private string _text;
        [SerializeField, GetSet("sortingOrder"), InspectorName("Order in Layer")] private int _sortingOrder;
        [SerializeField, GetSet("layerName")] private string _layerName;
#endif

        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private LocalizedString localization;

        public TextMesh mesh;

        public string text
        {
            get => mesh ? mesh.text : string.Empty;
            set
            {
                if (mesh) mesh.text = value;
#if UNITY_EDITOR
                _text = value;
#endif
            }
        }

        public int sortingOrder
        {
            get => _meshRenderer ? _meshRenderer.sortingOrder : 0;
            set
            {
                if (_meshRenderer) _meshRenderer.sortingOrder = value;
#if UNITY_EDITOR
                _sortingOrder = value;
#endif
            }
        }

        public string layerName
        {
            get => _meshRenderer ? _meshRenderer.sortingLayerName : string.Empty;
            set
            {
                if (_meshRenderer) _meshRenderer.sortingLayerName = value;
#if UNITY_EDITOR
                _layerName = value;
#endif
            }
        }

        protected virtual void Awake()
        {
            if (!mesh)
            {
                mesh = GetComponentInChildren<TextMesh>();
                _meshRenderer = mesh.GetComponent<MeshRenderer>();
            }
            else if (!_meshRenderer)
            {
                _meshRenderer = mesh.GetComponent<MeshRenderer>();
            }

            if (localization != null)
            {
                localization.StringChanged += value => text = value;
            }
        }
    }
}