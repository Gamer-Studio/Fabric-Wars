using FabricWars.Game.Items;
using FabricWars.Scenes.Board;
using FabricWars.Utils;
using UnityEngine;

namespace FabricWars.Game.Entities
{
    public class CollectableEntity : Entity
    {
        private static readonly int Cutoff = Shader.PropertyToID("cutoff");

        [Header("Components")]
        [SerializeField] private SpriteRenderer fillSprite;

        [Header("CollectableEntity Configuration")]
        public Item dropItem = Item.None;
        public int dropMaxAmount = 1;
        public int dropMinAmount = 1;
        public GaugeInt repeatCount = new(0, 1, 1);

        private void OnClick(bool val)
        {
            if (val)
            {
                repeatCount.value -= 1;
                var ratio = repeatCount.GetFillRatio();
                fillSprite.material.SetFloat(Cutoff, ratio);
                if (ratio >= 0)
                {
                    OnUse();
                }

                if (ratio <= 0)
                {
                    SendMessage("OnBreak", SendMessageOptions.DontRequireReceiver);
                }
            }
        }

        // Unity broadcast event interface

        public virtual void OnUse()
        {
            if(!ItemManager.instance) return;
            if(dropItem == null || dropItem == Item.None) return;
            
            var position = transform.position;
            int dropCount;

            if (dropMaxAmount == dropMinAmount) dropCount = dropMaxAmount;
            else
            {
                dropCount = Random.Range(dropMinAmount, dropMaxAmount);
            }
            
            for (var i = 0; i < dropCount; i++)
            {
                var x = Random.Range(-1, 2);
                var y = Random.Range(-1, 2);
                if (x == 0 && y == 0) y = 1;
            
                var item = ItemManager.instance.Create(dropItem, new Vector2(position.x + x * 0.5f, position.y + y * 0.5f));

                item.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            }
        }

        private void OnBreak()
        {
            Destroy(gameObject);
        }
    }
}