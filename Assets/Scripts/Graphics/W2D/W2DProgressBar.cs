using FabricWars.Utils.Serialization;
using UnityEngine;

namespace FabricWars.Graphics.W2D
{
    [AddComponentMenu("W2D/W2D ProgressBar")]
    public class W2DProgressBar : MonoBehaviour
    {
        private static readonly int Cutoff = Shader.PropertyToID("cutoff");

        public SpriteRenderer spriteRenderer;

        [SerializeField, GetSet("progress"), Range(1, 1000)] private int _progress;

        public int progress
        {
            get => _progress;
            set
            {
                if (value is < 1 or > 1000) return;

                _progress = value;
                spriteRenderer.material.SetFloat(Cutoff, (float)value / 1000);
            }
        }
    }
}