using UnityEngine;

namespace FabricWars.Game.Recipes
{
    [CreateAssetMenu(fileName = "new Entity Recipe", menuName = "Game/Item/Entity Recipe", order = 1)]
    public class EntityRecipe : Recipe
    {
        public GameObject entity;
        
    }
}