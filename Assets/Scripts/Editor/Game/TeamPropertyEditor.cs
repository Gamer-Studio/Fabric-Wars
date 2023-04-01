using FabricWars.Game;
using UnityEditor;
using UnityEngine;

namespace FabricWars.Editor.Game
{
    [CustomPropertyDrawer(typeof(Team))]
    public class TeamPropertyEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            {
                EditorGUI.LabelField(position, "Team", EditorStyles.boldLabel);
                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                

                EditorGUI.IntField(new Rect(position.position, new Vector2(EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight)), "id", 1);
                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                EditorGUI.LabelField(position, "Team", EditorStyles.boldLabel);
            }
            EditorGUI.EndChangeCheck();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) +
                   (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 2;
        }
    }
}