using System;

namespace FabricWars.Utils.Serialization
{
    [Serializable]
    public struct SerializablePair <K, V>
    {
        public K key;
        public V value;

        public SerializablePair(K key, V value)
        {
            this.key = key;
            this.value = value;
        }

        public void Deconstruct(out K key, out V value)
        {
            key = this.key;
            value = this.value;
        }
    }
}