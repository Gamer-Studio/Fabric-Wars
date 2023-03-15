using System.Collections.Generic;
using FabricWars.Game.Entities.Objects;
using UnityEngine;
using UnityEngine.Pool;

namespace FabricWars.Scenes.Board
{
    public sealed class EntityManager : MonoBehaviour
    {
        public static EntityManager instance { get; private set; }
        
        [SerializeField] private GameObject originalEntityPrefab;
        
        // Container
        [SerializeField] private Transform objectContainer;
        [SerializeField] private Transform releasedItemContainer;
        [SerializeField] private List<EntityObject> releasedObjects = new();
        [SerializeField] private List<EntityObject> pooledObjects = new();
        
        private ObjectPool<EntityObject> _pool;
        
        private void Awake()
        {
            if (instance)
            {
                Destroy(this);
                return;
            }

            _pool = new ObjectPool<EntityObject>(
                () => Instantiate(originalEntityPrefab, objectContainer).GetComponent<EntityObject>(),
                obj =>
                {
                    obj.transform.parent = objectContainer;
                    pooledObjects.Add(obj);
                    releasedObjects.Remove(obj);
                    obj.gameObject.SetActive(true);
                },
                obj =>
                {
                    
                }
                );

            instance = this;
        }
    }
}