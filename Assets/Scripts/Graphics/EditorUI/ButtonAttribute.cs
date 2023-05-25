using System;
using UnityEngine;

namespace FabricWars.Graphics.EditorUI
{
    /// <summary>
    /// Inspector button metadata
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ButtonAttribute : Attribute
    {
        public readonly string name;

        public ButtonAttribute(string name)
        {
            this.name = name;
        }

        public void OnGUI()
        {
            Debug.Log("H");
        }
    }
}