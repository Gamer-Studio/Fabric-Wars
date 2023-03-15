using System.Collections.Generic;
using FabricWars.Game.Items;
using UnityEngine;
using UnityEngine.Pool;

namespace FabricWars.Scenes.Board
{
    public sealed class ItemManager : MonoBehaviour
    {
        public static ItemManager instance { get; private set; }
        
        // Base Object
        [SerializeField] private GameObject originalItemPrefab;
        
        // Container
        [SerializeField] private Transform itemContainer;
        [SerializeField] private Transform releasedItemContainer;
        [SerializeField] private List<ItemObject> releasedObjects = new();
        [SerializeField] private List<ItemObject> pooledObjects = new();
        
        private ObjectPool<ItemObject> _pool;

        private void Awake()
        {
            if (instance)
            {
                Destroy(this);
                return;
            }

            _pool = new ObjectPool<ItemObject>(
                () =>
                { 
                    var item = Instantiate(originalItemPrefab, itemContainer).GetComponent<ItemObject>();
                    return item;
                },
                item =>
                {
                    item.transform.parent = itemContainer;
                    pooledObjects.Add(item);
                    releasedObjects.Remove(item);
                    item.gameObject.SetActive(true);
                },
                item =>
                {
                    pooledObjects.Remove(item);
                    releasedObjects.Add(item);
                    item.transform.parent = releasedItemContainer;
                    item.type = Item.None;
                    item.gameObject.SetActive(false);
                },
                item => Debug.Log($"item {item.name} destroy"),
                false, 10, int.MaxValue
                );

            instance = this;
        }

        public ItemObject Create(Item itemType, Vector2 position)
        {
            var obj = _pool.Get();
            obj.type = itemType;
            obj.transform.position = new Vector3(position.x, position.y, -1);
            return obj;
        }
        
        public void Release(ItemObject obj)
        {
            if (obj.gameObject.activeSelf)
            {
                _pool.Release(obj);
            }
        }

        public bool ItemHolding() => ItemObject.dragObj;
    }
}