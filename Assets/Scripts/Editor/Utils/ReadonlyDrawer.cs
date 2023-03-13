using FabricWars.Utils.Attributes;
using UnityEditor;
using UnityEngine;

namespace FabricWars.Editor.Utils
{
    [CustomPropertyDrawer(typeof(ReadonlyAttribute))]
    public class ReadonlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            var attribute = (ReadonlyAttribute)this.attribute;

            EditorGUI.BeginChangeCheck();

            EditorGUI.LabelField(position, label.text);
            
            //EditorGUI.PropertyField(position, property, label);
            
            EditorGUI.EndChangeCheck();
        }
    }
}