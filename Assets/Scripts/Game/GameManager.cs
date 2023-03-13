using System.Collections;
using FabricWars.Game.Items;
using UnityEngine;

namespace FabricWars.Game
{
    public sealed class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }

        public Camera mainCamera;

        public GameObject objectContainer;

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
            if (mainCamera)
            {
                var wheel = Input.GetAxis("Mouse ScrollWheel");
                if (Input.GetKey(KeyCode.LeftControl) &&
                    (mainCamera.orthographicSize, wheel) is not ((< 2, < 0) or (> 20, > 0)))
                {
                    mainCamera.orthographicSize += wheel * 2;
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

        private IEnumerator MoveCamera()
        {
            var bPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            for (;;)
            {
                var aPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

                var camTr = mainCamera.transform;
                var position = camTr.position;
                position = new Vector3(position.x + ((aPos.x - bPos.x) / 2), position.y + ((aPos.y - bPos.y) / 2),
                    position.z);
                camTr.position = position;

                bPos = aPos;

                yield return new WaitForEndOfFrame();
            }
        }
    }
}