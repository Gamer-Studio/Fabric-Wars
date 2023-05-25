using System;

namespace FabricWars.Utils.Serialization
{
    [Serializable]
    public class SerializableContainer <T> where T : new()
    {
        public T content;

        public SerializableContainer(T content)
        {
            this.content = content;
        }
        public SerializableContainer() : this(new()) {}
        
        public static implicit operator T(SerializableContainer<T> container) => container.content;
        public static implicit operator SerializableContainer<T>(T content) => new() { content = content };
    }
}