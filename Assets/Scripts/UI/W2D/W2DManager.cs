using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace FabricWars.UI.W2D
{
    [AddComponentMenu("W2D/W2D Manager")]
    public class W2DManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private CinemachineVirtualCamera vCam;

        private void Start()
        {
            if (!mainCamera)
            {
                mainCamera = Camera.main;
            }
        }

        [SerializeField] private Transform beforeTarget;
        public GameObject test;

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

                    if (beforeTarget != hit.transform)
                    {
                        if (beforeTarget)
                        {
                            beforeTarget.SendMessage("Hover", false, SendMessageOptions.DontRequireReceiver);
                        }

                        hit.transform.SendMessage("Hover", true, SendMessageOptions.DontRequireReceiver);
                    }
                    beforeTarget = hit.transform;
                    test.transform.position = new Vector3(hit.point.x, hit.point.y,test.transform.position.z);
                }
                else
                {
                    if (beforeTarget)
                    {
                        beforeTarget.SendMessage("Hover", false, SendMessageOptions.DontRequireReceiver);
                        beforeTarget = null;
                    }
                }
            }
        }
    }
}