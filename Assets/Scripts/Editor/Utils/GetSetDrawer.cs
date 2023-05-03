#if UNITY_EDITOR
using System.Reflection;
using FabricWars.Utils.Attributes;
using UnityEditor;
using UnityEngine;

namespace FabricWars.Editor.Utils
{
    [CustomPropertyDrawer(typeof(GetSetAttribute))]
    public sealed class GetSetDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = (GetSetAttribute)attribute;
            var isProperty = true;
            object value = null;

            EditorGUI.BeginChangeCheck();
            switch (property.type)
            {
                case "int":
                {
                    var i = EditorGUI.IntField(position, label, property.intValue);
                    if (i != property.intValue)
                    {
                        isProperty = false;
                        value = i;
                    }
                    
                    break;
                }
                case "string":
                {
                    var s = EditorGUI.TextField(position, label, property.stringValue);
                    if (!s.Equals(property.stringValue))
                    {
                        isProperty = false;
                        value = s;
                    }

                    break;
                }

                case "bool":
                {
                    var b = EditorGUI.Toggle(position, label, property.boolValue);
                    if (b != property.boolValue)
                    {
                        isProperty = false;
                        value = b;
                    }
                    
                    break;
                }

                case "byte":
                {
                    var i = EditorGUI.IntField(position, label, property.intValue);
                    if (i != property.intValue)
                    {
                        isProperty = false;
                        value = (byte)i;
                    }
                    
                    break;
                }
                
                default:
                {
                    Debug.Log(property.type);
                    EditorGUI.PropertyField(position, property, label);
                    break;
                }
            }
            
            if (EditorGUI.EndChangeCheck() && isProperty)
            {
                attr.dirty = true;
            }
            else if (attr.dirty || !isProperty)
            {
                var parent = GetParentObject(property.propertyPath, property.serializedObject.targetObject);

                var type = parent.GetType();
                var info = type.GetProperty(attr.name);

                if (info == null)
                    Debug.LogError("Invalid property name \"" + attr.name + "\"");
                else
                    info.SetValue(parent, isProperty ? fieldInfo.GetValue(parent) : value, null);

                attr.dirty = false;
            }
        }

        private static object GetParentObject(string path, object obj)
        {
            while (true)
            {
                var fields = path.Split('.');

                if (fields.Length == 1) return obj;

                var info = obj.GetType().GetField(fields[0], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                obj = info.GetValue(obj);

                path = string.Join(".", fields, 1, fields.Length - 1);
            }
        }
    }
}
#endif