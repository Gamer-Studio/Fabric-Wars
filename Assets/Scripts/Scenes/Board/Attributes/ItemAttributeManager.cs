using System.Collections.Generic;
using UnityEngine;

namespace FabricWars.Scenes.Board.Attributes
{
    public class ItemAttributeManager : MonoBehaviour
    {
        public static ItemAttributeManager instance { get; private set; }

        public List<ItemAttributeSlot> slots;

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