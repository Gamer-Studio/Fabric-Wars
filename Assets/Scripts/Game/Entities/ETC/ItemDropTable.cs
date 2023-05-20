using System;
using System.Collections.Generic;
using System.Linq;
using FabricWars.Game.Items;
using FabricWars.Utils;
using UnityEngine.Serialization;

namespace FabricWars.Game.Entities.ETC
{
    [Serializable]
    public class ItemDropTable
    {
        public GaugeInt durability = new(0, 1, 1);
        public List<ItemDropInfo> infos = new();

        public ItemDropTable()
        {
        }

        public ItemDropTable(GaugeInt repeat, params ItemDropInfo[] info)
        {
            durability = repeat;
            infos.AddRange(info);
        }

        public List<(Item item, int amount)> DropItem()
        {
            var result = new List<(Item item, int amount)>();
            if (durability.value == durability.min) return result;
            
            durability.value--;

            result.AddRange(
                from info in infos
                select info.Get()
                );

            return result;
        }
    }
}