using _2DMovementController.Scripts;
using LvlFacesManagement;
using UnityEngine;

namespace InLevelObjects
{
    public class MapRowRotator : MonoBehaviour
    {
        [SerializeField] protected PlayerEnum targetPlayer;
        [SerializeField] protected int mRotatedRow;
        protected bool MCanRotateMap;
        protected ILevelFacesManager MLvlFacesManager;

        protected virtual void Awake()
        {
            MLvlFacesManager = FindFirstObjectByType<LevelFacesManager>();
            if (MLvlFacesManager == null)
            {
                Debug.LogWarning("General Level Faces Manager must be available on Initialization");
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (HandlePlayerTrigger(other))
            {
                MCanRotateMap = true;
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
            return targetPlayer == PlayerEnum.Any || targetPlayer == pController.PlayerRole;
        }
        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerController>(out var pController))
            {
                if (pController.PlayerRole != targetPlayer || MCanRotateMap == false)
                {
                    return;
                }
                MCanRotateMap = false;
                Debug.Log("Can't rotate map!");
            } 
        }

        protected void Update()
        {
            if (!MCanRotateMap)
            {
                return;
            }
            ManageInputRotation();
        }

        protected virtual void ManageInputRotation()
        {
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
            MLvlFacesManager.RotateLevelRow(mRotatedRow, direction);
        }
    }
}