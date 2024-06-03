using _2DMovementController.Scripts;
using LvlFacesManagement;
using UnityEngine;

namespace InLevelObjects
{
    public class MapRotator : MonoBehaviour
    {
        [SerializeField] private PlayerEnum targetPlayer;
        [SerializeField] private int mRotatedRow;
        private bool _mCanRotateMap;
        
        
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (HandlePlayerTrigger(other))
            {
                _mCanRotateMap = true;
                Debug.Log("Can rotate map!");
            }
        }
        private bool HandlePlayerTrigger(Collider2D otherCol)
        {
            var isPlayer = otherCol.TryGetComponent<PlayerController>(out var pController);
            if (!isPlayer)
            {
                return false;
            }
            if (targetPlayer == PlayerEnum.Any)
            {
                return true;
            }
            return targetPlayer == pController.PlayerRole;
        }
        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerController>(out var pController))
            {
                if (pController.PlayerRole != targetPlayer || _mCanRotateMap == false)
                {
                    return;
                }
                _mCanRotateMap = false;
                Debug.Log("Can't rotate map!");
            } 
        }

        private void Update()
        {
            if (!_mCanRotateMap)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                CallRotation(1);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                CallRotation(-1);
            }
        }

        private void CallRotation(int direction)
        {
            var lvlFacesManager = FindFirstObjectByType<LevelFacesManager>();
            if (!lvlFacesManager)
            {
                return;
            }
            lvlFacesManager.RotateLevelRow(mRotatedRow, direction);
        }
    }
}