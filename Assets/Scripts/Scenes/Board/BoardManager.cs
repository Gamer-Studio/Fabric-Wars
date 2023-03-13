using System.Collections;
using Cinemachine;
using FabricWars.Game.Items;
using UnityEngine;

namespace FabricWars.Scenes.Board
{
    public sealed class BoardManager : MonoBehaviour
    {
        public static BoardManager instance { get; private set; }

        public Camera mainCamera;
        [SerializeField] private Rigidbody2D cameraTargetBody;
        public CinemachineVirtualCamera virtualCamera;

        private void Awake()
        {
            if (instance)
            {
                Destroy(this);
                return;
            }

            if (!mainCamera) mainCamera = Camera.main;

            instance = this;
        }

        private Coroutine _moveCamera;

        private void Update()
        {
            if (virtualCamera)
            {
                var wheel = Input.GetAxis("Mouse ScrollWheel");
                if (Input.GetKey(KeyCode.LeftControl) &&
                    (virtualCamera.m_Lens.OrthographicSize, wheel) is not ((< 2, < 0) or (> 20, > 0)))
                {
                    virtualCamera.m_Lens.OrthographicSize += wheel * 2;
                }

                switch (Input.GetKey(KeyCode.Mouse2))
                {
                    case true when !Input.GetKey(KeyCode.LeftControl) && _moveCamera == null:
                        _moveCamera = StartCoroutine(MoveCamera());
                        break;

                    case false when _moveCamera != null:
                        StopCoroutine(_moveCamera);
                        _moveCamera = null;
                        break;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                ItemObject.StopDrag();
            }
        }

        public float cameraMoveSpeed = 100;

        private IEnumerator MoveCamera()
        {
            var bPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            for (;;)
            {
                var aPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

                cameraTargetBody.AddForce(new Vector2((bPos.x - aPos.x) * cameraMoveSpeed, (bPos.y - aPos.y) * cameraMoveSpeed));

                bPos = aPos;

                yield return new WaitForEndOfFrame();
            }
        }
    }
}