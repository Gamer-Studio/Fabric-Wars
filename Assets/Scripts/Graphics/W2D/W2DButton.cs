using FabricWars.Utils.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace FabricWars.Graphics.W2D
{
    [RequireComponent(typeof(Collider2D)), AddComponentMenu("W2D/W2D Button")]
    public class W2DButton : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public TextMesh textMesh;
        public UnityEvent onClick;
        public UnityEvent<bool> onHover;
        [SerializeField, GetSet("text")] private string _text;

        public string text
        {
            get => _text;
            set
            {
                if(!textMesh) return;

                textMesh.text = _text = value;
            }
        }

        private void OnClick(int value)
        {
            if (value == 1) onClick.Invoke();
        }

        private void Hover(bool active) => onHover.Invoke(active);
    }
}