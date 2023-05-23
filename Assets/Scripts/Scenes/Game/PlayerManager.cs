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
        public KeyCode openInventory, interaction1, interaction2;

        private void Awake()
        {
            Settings.keyMappings.TryGetValue("playerManager.openInventory", out openInventory);
            Settings.keyMappings.TryGetValue("playerManager.interaction1", out interaction1);
            Settings.keyMappings.TryGetValue("playerManager.interaction2", out interaction2);
        }

        private void Update()
        {
            if (Input.GetKeyDown(openInventory)) inventoryViewer.open = !inventoryViewer.open;

            if (currentPlayer != null)
            {
                if (Input.GetKeyDown(interaction1)) currentPlayer.Interaction_1();
                if (Input.GetKeyDown(interaction2)) currentPlayer.Interaction_2();
            }
        }
    }
}