using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.Util;

namespace FabricWars.Scenes.Game
{
    public sealed class PlayerManager : ComponentSingleton<PlayerManager>
    {
        public PlayerInput input;
        public Player currentPlayer;

        [Header("Viewer Panels")] public InventoryViewer inventoryViewer;

        [Header("KeyBinds"), SerializeField] public KeyCode interaction1, interaction2;

        private void Awake()
        {
            Settings.keyMappings.TryGetValue("playerManager.interaction1", out interaction1);
            Settings.keyMappings.TryGetValue("playerManager.interaction2", out interaction2);

            if (input == null) input = PlayerInput.all[0];

            input.onActionTriggered += context =>
            {
                if (context.action.name == "Navigate" && currentPlayer != null)
                    currentPlayer.NavigateTrigger(context.ReadValue<Vector2>());
            };
        }

        private void Update()
        {
            if (currentPlayer != null)
            {
                if (Input.GetKeyDown(interaction1)) currentPlayer.Interaction_1();
                if (Input.GetKeyDown(interaction2)) currentPlayer.Interaction_2();
            }
        }
    }
}