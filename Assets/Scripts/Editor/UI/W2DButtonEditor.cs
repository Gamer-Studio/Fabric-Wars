using System;
using FabricWars.Graphics.W2D;
using UnityEditor;
using UnityEngine;

namespace FabricWars.Editor.UI
{
    [CustomEditor(typeof(W2DButton))]
    public class W2DButtonEditor : UnityEditor.Editor
    {
        private Collider2D collider;
        private SpriteRenderer spriteRenderer;

        private void OnEnable()
        {
            var obj = (W2DButton)target;
            collider = obj.GetComponent<Collider2D>();
            spriteRenderer = obj.spriteRenderer;
        }

        private void OnSceneGUI()
        {
            if(!collider || !spriteRenderer) return;

            switch (collider)
            {
                case BoxCollider2D col:
                {
                    col.offset = new Vector2(0, 0);
                    col.size = spriteRenderer.size;
                    break;
                }
            }
        }
    }
}