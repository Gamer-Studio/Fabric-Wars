using FabricWars.Game;
using FabricWars.Utils.Extensions;
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
                var team = (Team)fieldInfo.GetValue(property.serializedObject.targetObject);
                position.y -= EditorGUIUtility.singleLineHeight;
                
                EditorGUI.LabelField(position, "Team", EditorStyles.boldLabel);
                EditorGUI.LabelField(new Rect(position)
                {
                    x = position.width / 2
                }, team.Name);

                var id = EditorGUI.IntField(
                    new Rect(position)
                    {
                        height = EditorGUIUtility.singleLineHeight,
                        y = position.y +
                            (EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing)
                    }, "Id",
                    team.id, EditorStyles.numberField);

                fieldInfo.SetValue(property.serializedObject.targetObject, new Team((byte)id));
            }
            EditorGUI.EndChangeCheck();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + (EditorGUIUtility.singleLineHeight +
                                                              EditorGUIUtility.standardVerticalSpacing);
        }
    }
}