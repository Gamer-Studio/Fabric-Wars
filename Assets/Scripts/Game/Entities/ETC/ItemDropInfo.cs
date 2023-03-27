using System;
using FabricWars.Game.Items;
using Random = UnityEngine.Random;

namespace FabricWars.Game.Entities.ETC
{
    [Serializable]
    public class ItemDropInfo
    {
        public Item item = null;
        public int max = 1;
        public int min = 1;

        public ItemDropInfo() { }

        public ItemDropInfo(Item item, int max, int min)
        {
            this.item = item;
            this.max = max;
            this.min = min;
        }

        public virtual (Item item, int amount) Get()
        {
            return (item, Random.Range(min, max + 1));
        }
    }
}