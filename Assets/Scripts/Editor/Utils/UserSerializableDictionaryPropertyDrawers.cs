#if UNITY_EDITOR
using FabricWars.Utils.Serialization;
using UnityEditor;

namespace FabricWars.Editor.Utils
{
    [CustomPropertyDrawer(typeof(SerializableDictionary<,>))]
    public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}

    [CustomPropertyDrawer(typeof(SerializableDictionary<,,>))]
    public class AnySerializableDictionaryStoragePropertyDrawer: SerializableDictionaryStoragePropertyDrawer {}
}
#endif