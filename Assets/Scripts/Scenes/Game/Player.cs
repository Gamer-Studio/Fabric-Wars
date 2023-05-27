using System.Collections;
using System.Collections.Generic;
using FabricWars.Game;
using FabricWars.Game.Entities;
using FabricWars.Game.Items;
using UnityEngine;

namespace FabricWars.Scenes.Game
{
    public class Player : Entity
    {
        public static readonly List<Player> players = new();
        
        public Inventory inventory;
        [SerializeField] private Vector2 direction;
        [SerializeField] private bool isMoving = false;
        [SerializeField] private Rigidbody2D body;
        [SerializeField] private LineRenderer line;
        
        private void Awake()
        {
            if (body == null) body = GetComponent<Rigidbody2D>();

            players.Add(this);

            StartCoroutine(Move());
            Settings.Save();
        }

        private void OnDestroy()
        {
            players.Remove(this);
        }

        internal void NavigateTrigger(Vector2 navDir)
        {
            if (navDir == Vector2.zero) isMoving = false;
            else
            {
                direction = navDir;
                isMoving = true;
                if(line != null) line.SetPosition(1, navDir * 2);
            }
        }

        public float speed = 4;
        private IEnumerator Move()
        {
            while (true)
            {
                if(isMoving) body.AddForce(direction * speed);
                yield return new WaitForFixedUpdate();
            }
        }

        public void OnItemHit(ItemObject itemObject)
        {
            if (inventory.TryAddItem(itemObject.type, 1) < 1)
            {
                ItemManager.instance.Release(itemObject);
            }
        }

        public void Interaction_1()
        {
            Debug.Log(direction);
        }

        public void Interaction_2()
        {
            
        }
    }
}