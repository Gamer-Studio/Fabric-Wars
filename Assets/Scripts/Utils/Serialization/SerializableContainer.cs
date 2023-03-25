using System;

namespace FabricWars.Utils.Serialization
{
    [Serializable]
    public class SerializableContainer <T>
    {
        public T content;

        public SerializableContainer(T content)
        {
            this.content = content;
        }
        public SerializableContainer() : this(default){}
        
        public static explicit operator T(SerializableContainer<T> container) => container.content;
        public static explicit operator SerializableContainer<T>(T content) => new() { content = content };
    }
}