using System.Collections.Generic;
using Cinemachine;
using LvlFacesManagement;
using UnityEngine;

namespace Camera
{
    public class CameraMovementController : MonoBehaviour
    {
        private IPlayerActiveManager _mPlayerActiveManager;
        [SerializeField]private CinemachineVirtualCamera _mBaseCamera;
        
        [SerializeField]private List<LevelFace> MPlayerLevelTargets;
        private Dictionary<PlayerEnum, GameObject> _mCameraTargets = new();

        private void Awake()
        {
            _mPlayerActiveManager = FindFirstObjectByType<PlayerActiveManager>();
            _mPlayerActiveManager.ChangePlayerActive += ChangePlayerTarget;
            _mCameraTargets.Clear();
            foreach (var lvlTargets in MPlayerLevelTargets)
            {
                _mCameraTargets.Add(lvlTargets.PlayerOwner, lvlTargets.gameObject);
            }
        }

        private void ChangePlayerTarget(PlayerEnum newTarget)
        {
            Debug.Log("Event Received!");
            if (_mCameraTargets.Count == 0)
            {
                return;
            }

            if (!_mCameraTargets.ContainsKey(newTarget))
            {
                return;
            }

            _mBaseCamera.Follow = _mCameraTargets[newTarget].transform;
        }
    }
}