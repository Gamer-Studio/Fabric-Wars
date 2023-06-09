using UnityEngine;

namespace FabricWars.Utils.Serialization
{
    public sealed class GetSetAttribute : PropertyAttribute
    {
        public readonly string name;
        public bool dirty;

        public GetSetAttribute(string name)
        {
            this.name = name;
        }
    }
}