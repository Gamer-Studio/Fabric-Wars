using System.Linq;
using FabricWars.Utils.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FabricWars.Editor.Utils
{
    public class ShadowUtil : EditorWindow
    {
        [MenuItem("Util/ShadowUtil")]
        private static void ShowWindow()
        {
            var window = GetWindow<ShadowUtil>();
            window.titleContent = new GUIContent("Shadow Utility");
            window.Show();
        }

        private ColliderType _colliderType = ColliderType.Sprite;
        private Sprite _sprite;
        private SpriteRenderer _renderer;
        private ShadowCaster2D _shadowCaster2D;
        
        private void OnGUI()
        {
            _colliderType = (ColliderType)EditorGUILayout.EnumPopup("Collider Type", _colliderType);
            Vector3[] shape = null;
            
            switch (_colliderType)
            {
                case ColliderType.Sprite:
                {
                    _sprite = (Sprite)EditorGUILayout.ObjectField("Collider Sprite", _sprite, typeof(Sprite), true);
                    if (_sprite != null) shape = (from point in _sprite.GetPhysicsShape(0) select point.ToVector3()).ToArray();
                    break;
                }

                case ColliderType.SpriteRenderer:
                {
                    _renderer = (SpriteRenderer)EditorGUILayout.ObjectField("Collider Sprite", _renderer, typeof(SpriteRenderer), true);
                    if(_renderer != null)
                    {
                        shape = (from point in _renderer.sprite.GetPhysicsShape(0) select point.ToVector3()).ToArray();
                        if (_renderer.gameObject != null && _shadowCaster2D == null)
                        {
                            _renderer.gameObject.TryGetComponent(out _shadowCaster2D);
                        }
                    }
                    
                    break;
                }
            }

            _shadowCaster2D = (ShadowCaster2D)EditorGUILayout.ObjectField("Shadow", _shadowCaster2D, typeof(ShadowCaster2D), true);
            
            if (GUILayout.Button("Update Shadow Collider"))
            {
                if (shape == null)
                {
                    Debug.Log("shape is null");
                    return;
                }
                
                _shadowCaster2D.SetShapePath(shape);
            }
        }

        public enum ColliderType
        {
            Sprite, SpriteRenderer
        }
    }
}