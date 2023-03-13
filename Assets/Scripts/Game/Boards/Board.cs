using System.Collections;
using FabricWars.Utils.Attributes;
using UnityEngine;

namespace FabricWars.Game.Boards
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        public bool canCatch = true;

        [SerializeField, GetSet("boardName")] private string _boardName;

        public string boardName
        {
            get => _boardName;
            set
            {
                name = value.Length > 0 ? $"{GetType().Name}_{_boardName}" : GetType().Name;
                _boardName = value;
            }
        }

        protected virtual void Start()
        {
            if (!mainCamera)
            {
                mainCamera = Camera.main;
            }
        }

        private Coroutine _moveBoard;

        private void OnClick(int active)
        {
            switch (active)
            {
                case 1 when _moveBoard == null:
                    _moveBoard = StartCoroutine(MoveBoard());
                    break;
                case 0 when _moveBoard != null:
                    StopCoroutine(_moveBoard);
                    _moveBoard = null;
                    break;
            }
        }

        private IEnumerator MoveBoard()
        {
            var bPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            for (;;)
            {
                var aPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                if (!bPos.Equals(aPos))
                {
                    var position = transform.position;
                    position = new Vector2(position.x + aPos.x - bPos.x, position.y + aPos.y - bPos.y);
                    transform.position = position;

                    bPos = aPos;
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }
}