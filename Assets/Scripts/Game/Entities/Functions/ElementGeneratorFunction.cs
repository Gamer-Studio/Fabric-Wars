using System.Collections;
using FabricWars.Game.Elements;
using FabricWars.Scenes.Game.Elements;
using FabricWars.Utils.Serialization;
using UnityEngine;

namespace FabricWars.Game.Entities.Functions
{
    [CreateAssetMenu(fileName = "Element Generator", menuName = "Game/Function/Element Production")]
    public class ElementGeneratorFunction : EntityFunction
    {
        public SerializablePair<Element, int>[] products;
        public float cooldown;

        public override IEnumerator GetFunction()
        {
            if(!ElementManager.instance) yield break;
            
            while (true)
            {
                foreach (var (product, count) in products)
                {
                    ElementManager.instance.AddElementValue(product, count);
                }

                yield return new WaitForSeconds(cooldown);
            }
        }
    }
}