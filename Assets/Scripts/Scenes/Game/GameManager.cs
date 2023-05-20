using System;
using System.Collections;
using Cinemachine;
using FabricWars.Game.Items;
using FabricWars.Utils.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace FabricWars.Scenes.Game
{
    public sealed class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }
        
        [Header("Managers")] 
        public PlayerManager playerManager;
        
        private Camera _camera;
        [Header("Components")]
        [SerializeField] private Rigidbody2D cameraTargetBody;
        public CinemachineVirtualCamera virtualCamera;

        private void Awake()
        {
            if (instance)
            {
                Destroy(this);
                return;
            }

            if (!_camera) _camera = Camera.main;

            instance = this;
        }

        private Coroutine _moveCamera;
        [SerializeField] private float maxZoomSize = 10;
        [SerializeField] private float minZoomSize = 2;
        [SerializeField] private float zoomSpeed = 2;

        private void Start()
        {
            StartCoroutine(TimeUpdater());
        }

        private void Update()
        {
            if (virtualCamera)
            {
                var wheel = Input.GetAxis("Mouse ScrollWheel");
                if (Input.GetKey(KeyCode.LeftControl) &&
                    !((virtualCamera.m_Lens.OrthographicSize < minZoomSize && wheel < 0) ||
                     (virtualCamera.m_Lens.OrthographicSize > maxZoomSize && wheel > 0))
                    )
                {
                    virtualCamera.m_Lens.OrthographicSize += wheel * zoomSpeed;
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
            var bPos = _camera.ScreenToWorldPoint(Input.mousePosition);

            for (;;)
            {
                var aPos = _camera.ScreenToWorldPoint(Input.mousePosition);

                cameraTargetBody.AddForce(new Vector2((bPos.x - aPos.x) * cameraMoveSpeed, (bPos.y - aPos.y) * cameraMoveSpeed));

                bPos = aPos;

                yield return new WaitForEndOfFrame();
            }
        }
        
        #region time manager
        public UnityEvent<bool> onPause;

        [SerializeField, GetSet("pause")] private bool _pause;

        public bool pause
        {
            get => _pause;
            set
            {
                _pause = value;
                onPause.Invoke(value);
            }
        }

        private IEnumerator TimeUpdater()
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
            }
        }
        #endregion
    }
}