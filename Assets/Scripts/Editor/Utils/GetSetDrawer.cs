#if UNITY_EDITOR
using System.Reflection;
using FabricWars.Game.Items;
using FabricWars.Utils;
using FabricWars.Utils.Serialization;
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

                /*
                case "PPtr<$Item>":
                {
                    EditorGUI.ObjectField(position, property, typeof(Item));
                    break;
                }
                */
                
                default:
                {
                    EditorGUI.PropertyField(position, property, label);
                    //Debug.Log(property.type);
                    break;
                }
            }
            
            if (EditorGUI.EndChangeCheck() && isProperty)
            {
                attr.dirty = true;
            }
            else if (attr.dirty || !isProperty)
            {
                var parent = ObjectUtil.GetParentObject(property.propertyPath, property.serializedObject.targetObject);

                var type = parent.GetType();
                var info = type.GetProperty(attr.name);

                if (info == null)
                    Debug.LogError("Invalid property name \"" + attr.name + "\"");
                else
                    info.SetValue(parent, isProperty ? fieldInfo.GetValue(parent) : value, null);

                attr.dirty = false;
            }
        }
    }
}
#endif