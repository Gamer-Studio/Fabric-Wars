using System;
using FabricWars.Game.Elements;
using FabricWars.Scenes.Board;
using FabricWars.Utils.Serialization;
using UnityEngine;
using UnityEngine.Localization.Metadata;
using UnityEngine.Localization.Tables;

namespace FabricWars.Game.Items
{
    [CreateAssetMenu(fileName = "new Item", menuName = "Game/Item/Basic Item", order = 0)]
    public partial class Item : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<string, StringTable> contentTable;

        public int weight = 0;
        public int maxAmount = 1;
        public bool canCatch = true;
        public Sprite sprite;
        public SerializableDictionary<Element, int> elements;

        public ItemObject Create(Vector2 position)
        {
            if (!ItemManager.instance) throw new NullReferenceException("ItemManager is not activated");
            return ItemManager.instance.Create(this, position);
        }

        public string GetLocalizedName()
        {
            if (contentTable.TryGetValue("ko", out StringTable table))
                return table.GetEntry($"{name.ToLower()}.name").GetLocalizedString();
            return "";
        }

        public string GetLocalizedDescription()
        {
            if(contentTable.TryGetValue("ko", out StringTable table))
                return table.GetEntry($"{name.ToLower()}.description").GetLocalizedString();
            return "";
        } 
    }
}