﻿using System;
using System.Collections;
using System.Collections.Generic;
using FabricWars.Game;
using FabricWars.Game.Entities;
using FabricWars.Game.Items;
using FabricWars.Utils.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace FabricWars.Scenes.Board
{
    public class Player : Entity
    {
        public static readonly List<Player> players = new();
        public Inventory inventory;
        [SerializeField] private Vector2 direction;
        [SerializeField] private Rigidbody2D body;
        
        protected override void Awake()
        {
            base.Awake();

            if (body == null) body = GetComponent<Rigidbody2D>();
            
            EventSystem.current.GetComponent<PlayerInput>().onActionTriggered += context =>
            {
                if (context.action.name == "Navigate")
                {
                    direction = context.ReadValue<Vector2>();
                }
            };

            players.Add(this);

            StartCoroutine(Move());
            Settings.Save();
        }

        private void OnDestroy()
        {
            players.Remove(this);
        }

        public float speed = 4;
        private IEnumerator Move()
        {
            while (true)
            {
                body.AddForce(direction.Multiply(speed));
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
    }
}