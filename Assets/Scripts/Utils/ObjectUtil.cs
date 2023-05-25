using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FabricWars.Utils
{
    public static class ObjectUtil
    {
        public static object GetParentObject(string path, object obj)
        {
            while (true)
            {
                var fields = path.Split('.');

                if (fields.Length == 1) return obj;

                var info = obj.GetType().GetField(fields[0],
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                obj = info.GetValue(obj);

                path = string.Join(".", fields, 1, fields.Length - 1);
            }
        }
        
        // TODO: 위 코드는 전에 웹에서 스크랩해온건데 비효율적으로 보여서 개선중이지만 뭐가 문젠지 아래 코드가 어디가 다른건지 모르겠음..
        public static object GetParentObject(SerializedProperty property)
        {
            object obj = property.serializedObject.targetObject;
            var fields = property.propertyPath.Split('.');
            
            foreach (var field in fields)
            {
                var info = obj.GetType().GetField(field,BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (info == null) break;
                Debug.Log(info.Name);
                
                var temp = info.GetValue(obj);
                if (temp != null) obj = temp;
            }

            return obj;
        }
    }
}