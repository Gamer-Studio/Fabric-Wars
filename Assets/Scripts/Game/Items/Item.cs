﻿using System;
using FabricWars.Utils.Serialization;
using UnityEngine;

namespace FabricWars.Game.Items
{
    [CreateAssetMenu(fileName = "new Item", menuName = "Game/default Item", order = 0)]
    public class Item : ScriptableObject
    {
        public int weight = 0;
        public Sprite sprite;
        public bool canCatch = true;
        public SerializableDictionary<ItemAttribute, int> attributes;

        public ItemObject Create(Vector2 position)
        {
            if (!ItemManager.instance) throw new NullReferenceException("ItemManager is not activated");

            return ItemManager.instance.Create(this, position);
        }

        public string GetDescription()
        {
            /* TODO
             * Get item description from localization {Content} table
             * Key - {item.name}.description
             */
            return "";
        }
    }
}