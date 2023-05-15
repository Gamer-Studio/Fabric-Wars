using System;
using UnityEngine;

namespace FabricWars.Scenes.Board
{
    public sealed class PlayerManager : MonoBehaviour
    {
        public PlayerManager instance { get; private set; }

        public Player currentPlayer;
        
        [Header("Viewer Panels")] 
        public InventoryViewer inventoryViewer;

        [Header("KeyBinds")] 
        public KeyCode openInventory;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            Settings.keyMappings.TryGetValue("playerManager_openInventory", out openInventory);

            instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(openInventory)) inventoryViewer.open = !inventoryViewer.open;
        }
    }
}