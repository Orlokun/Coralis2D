using LvlFacesManagement;
using UnityEngine;
namespace InLevelObjects
{
    public class PlayerBasedMapRowRotator : MapRowRotator
    {
        protected override void Awake()
        {
            base.Awake();
            if (targetPlayer == PlayerEnum.Any)
            {
                Debug.LogError($"[PlayerBasedMapRowRotator] Target Player must be assigned to rotator named {gameObject.name}");
            }
        }
        protected override void ManageInputRotation()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                CallPlayerBasedRotation(1);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                CallPlayerBasedRotation(-1);
            }
        }

        private void CallPlayerBasedRotation(int direction)
        {
            Debug.Log("Player Based Rotation Activated");
            MLvlFacesManager.RotatePlayerBaseObjectsInRow(targetPlayer, mRotatedRow, direction);
        }
    }
}