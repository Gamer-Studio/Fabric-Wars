using System.Collections.Generic;
using System.Reflection;
using FabricWars.Graphics.EditorUI;
using UnityEditor;
using UnityEngine;

namespace FabricWars.Editor.UI
{
    [CustomEditor(typeof(Object), true), CanEditMultipleObjects]
    public class EditorButtonDrawer : UnityEditor.Editor
    {
        private readonly List<(MethodInfo, ButtonAttribute)> _buttons = new();

        private void OnEnable()
        {
            var methods = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static |
                                                      BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                var attribute = method.GetCustomAttribute<ButtonAttribute>();
                
                if (attribute != null) _buttons.Add((method, attribute));
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            foreach (var (method, attribute) in _buttons)
                if (GUILayout.Button(attribute.name))
                    method.Invoke(target, null);
        }
    }
}