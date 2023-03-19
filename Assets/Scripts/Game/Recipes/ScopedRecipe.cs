using FabricWars.Game.Elements;
using FabricWars.Utils.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

namespace FabricWars.Game.Recipes
{
    [CreateAssetMenu(fileName = "new Scoped Recipe", menuName = "Game/Recipe/Scoped Recipe", order = 1)]
    public class ScopedRecipe : Recipe
    {
        public GameObject entity;
        public SerializablePair<Element, int>[] consumes;

        public SerializablePair<Element, int>[] scopes;
    }
}