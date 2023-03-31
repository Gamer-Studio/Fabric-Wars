using System.Linq;
using FabricWars.Game.Entities.ETC;
using FabricWars.Scenes.Board;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FabricWars.Game.Entities
{
    public class CollectableEntity : Entity
    {
        private static readonly int Cutoff = Shader.PropertyToID("cutoff");

        [Header("Components")] [SerializeField]
        private SpriteRenderer fillSprite;

        [Header("CollectableEntity Configuration")]
        public ItemDropTable dropTable = new();

        private void OnClick(bool val)
        {
            if (val) OnUse();
        }

        // Unity broadcast event interface

        public virtual void OnUse()
        {
            if (!ItemManager.instance) return;

            var position = transform.position;

            foreach (var (dropItem, amount) in dropTable.DropItem())
            {
                for (var i = 0; i < amount; i++)
                {
                    var x = Random.Range(-1, 2);
                    var y = Random.Range(-1, 2);
                    if (x == 0 && y == 0) y = 1;

                    var item = ItemManager.instance.Create(dropItem,
                        new Vector2(position.x + x * 0.5f, position.y + y * 0.5f));

                    item.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                }
            }

            fillSprite.material.SetFloat(Cutoff, dropTable.repeatCount.GetFillRatio());

            if (dropTable.repeatCount.value == dropTable.repeatCount.min)
                SendMessage("OnBreak", SendMessageOptions.DontRequireReceiver);
        }

        private void OnBreak()
        {
            Destroy(gameObject);
        }
    }
}