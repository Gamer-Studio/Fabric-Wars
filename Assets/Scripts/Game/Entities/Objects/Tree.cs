using FabricWars.Game.Entities.Objects;
using FabricWars.Game.Items;
using FabricWars.Scenes.Board;
using UnityEngine;

namespace FabricWars.Game.Objects
{
    public class Tree : CollectableEntity
    {
        private void OnUse()
        {
            if(!ItemManager.instance) return;
            
            var position = transform.position;

            var x = Random.Range(-1, 2);
            var y = Random.Range(-1, 2);
            if (x == 0 && y == 0) y = 1;
            
            var item = ItemManager.instance.Create(Item.Log, new Vector2(position.x + x * 0.5f, position.y + y * 0.5f));

            item.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        }
        
        private void OnBreak()
        {
            Destroy(gameObject);
        }
    }
}