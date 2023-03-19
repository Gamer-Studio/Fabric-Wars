using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace FabricWars.Graphics.W2D
{
    [AddComponentMenu("W2D/W2D Manager")]
    public class W2DManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        private void Start()
        {
            if (!mainCamera)
            {
                mainCamera = Camera.main;
            }
        }

        [SerializeField] private Transform _beforeTarget;
        internal Transform beforeTarget => _beforeTarget;

        private void Update()
        {
            if (mainCamera)
            {
                
                var ray = mainCamera.ViewportPointToRay(mainCamera.ScreenToViewportPoint(Input.mousePosition));
                var hit = Physics2D.Raycast(ray.origin, Vector2.zero);
                
                if (hit.transform)
                {
                    if (Input.GetMouseButtonDown(0))
                        hit.transform.SendMessage("OnClick", true, SendMessageOptions.DontRequireReceiver);
                    if (Input.GetMouseButtonUp(0))
                        hit.transform.SendMessage("OnClick", false, SendMessageOptions.DontRequireReceiver);

                    if (_beforeTarget != hit.transform)
                    {
                        if (_beforeTarget)
                        {
                            _beforeTarget.SendMessage("Hover", false, SendMessageOptions.DontRequireReceiver);
                        }

                        hit.transform.SendMessage("Hover", true, SendMessageOptions.DontRequireReceiver);
                    }
                    _beforeTarget = hit.transform;
                }
                else
                {
                    if (_beforeTarget)
                    {
                        _beforeTarget.SendMessage("Hover", false, SendMessageOptions.DontRequireReceiver);
                        _beforeTarget = null;
                    }
                }
            }
        }
    }
}