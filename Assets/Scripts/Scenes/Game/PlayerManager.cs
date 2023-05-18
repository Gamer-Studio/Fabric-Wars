using System;
using UnityEngine;
using UnityEngine.ResourceManagement.Util;

namespace FabricWars.Scenes.Game
{
    public sealed class PlayerManager : ComponentSingleton<PlayerManager>
    {
        public Player currentPlayer;
        
        [Header("Viewer Panels")] 
        public InventoryViewer inventoryViewer;

        [Header("KeyBinds")] 
        public KeyCode openInventory;

        private void Awake()
        {
            Settings.keyMappings.TryGetValue("playerManager_openInventory", out openInventory);
        }

        private void Update()
        {
            if (Input.GetKeyDown(openInventory)) inventoryViewer.open = !inventoryViewer.open;
        }
    }
}