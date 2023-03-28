using System;
using System.Collections.Generic;

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

        public static implicit operator SerializablePair<K, V>((K key, V value) pair) => new (pair.key, pair.value);
        public static implicit operator KeyValuePair<K, V>(SerializablePair<K, V> pair) => new (pair.key, pair.value);
        public static implicit operator SerializablePair<K, V>(KeyValuePair<K, V> pair) => new (pair.Key, pair.Value);
    }
}