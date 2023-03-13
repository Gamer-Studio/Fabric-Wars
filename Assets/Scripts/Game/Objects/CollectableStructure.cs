using FabricWars.Utils;
using UnityEngine;

namespace FabricWars.Game.Objects
{
    public class CollectableStructure : Structure
    {
        private static readonly int Cutoff = Shader.PropertyToID("cutoff");

        [SerializeField] private SpriteRenderer fillSprite;

        public GaugeInt storage = new GaugeInt(0, 1, 1);

        protected override void Awake()
        {
            base.Awake();

            storage.onChange.AddListener(gauge => { fillSprite.material.SetFloat(Cutoff, gauge.GetFillRatio()); });
        }

        private void OnClick(bool val)
        {
            if (val)
            {
                storage.value -= 1;
                var ratio = storage.GetFillRatio();
                if (ratio >= 0)
                {
                    SendMessage("OnUse", SendMessageOptions.DontRequireReceiver);
                }

                if (ratio <= 0)
                {
                    SendMessage("OnBreak", SendMessageOptions.DontRequireReceiver);
                }
            }
        }

        // Unity broadcast event interface

        private void OnUse()
        {
        }

        private void OnBreak()
        {
        }
    }
}