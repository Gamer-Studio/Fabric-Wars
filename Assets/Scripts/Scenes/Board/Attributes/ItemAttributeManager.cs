using System.Collections.Generic;
using FabricWars.Game.Items;
using FabricWars.Utils;
using FabricWars.Utils.Serialization;
using UnityEngine;

namespace FabricWars.Scenes.Board.Attributes
{
    public class ItemAttributeManager : MonoBehaviour
    {
        public static ItemAttributeManager instance { get; private set; }

        public List<SerializablePair<ItemAttribute, GaugeInt>> attributeInventory;
        [SerializeField] private List<ItemAttributeSlot> slots;

        private void Awake()
        {
            if (instance)
            {
                Destroy(this);
                return;
            }
            
            instance = this;
        }
    }
}