using System.Collections.Generic;
using TransferObject;
using TransferObject.Interfaces_Data;
using UnityEngine;

namespace LvlFacesManagement
{
    public class LevelFaceRow : MonoBehaviour, ILevelFaceRow
    {

        /// <summary>
        /// Defines the row in the level and Which player 'owns' this lvl face
        /// </summary>
        public int RowIndex => mRowIndex;
        [SerializeField] private int mRowIndex;
        private PlayerEnum _mOwner;

        /// <summary>
        /// Keep track of current objects in the row
        /// </summary>
        public List<IPlayerBasedTransferableObject> PlayerTransferableObjects => _mPlayerTransferableObjects;
        private List<IPlayerBasedTransferableObject> _mPlayerTransferableObjects = new();
        [SerializeField] private List<PlayerBasedTransferableObject> initPlayerTransferableObjects;

        
        public List<ISimpleTransferableObject> ActiveTransferableObjects => _mActiveTransferableObjects;
        private List<ISimpleTransferableObject> _mActiveTransferableObjects = new();
        [SerializeField] private List<TransferableObject> InitTransferableObjects;

        public bool IsInit => _mIsInit;
        private bool _mIsInit;
        public void Init(PlayerEnum owner)
        {
            if (_mIsInit)
            {
                return;
            }
            _mOwner = owner;
            PopulateInitialObjects();
            _mIsInit = true;
        }
        
        private void PopulateInitialObjects()
        {
            PopulateBaseTransferableObjects();
            PopulatePlayerTransferableObjects();
        }

        private void PopulateBaseTransferableObjects()
        {
            _mActiveTransferableObjects.Clear();
            foreach (var initTransferableObject in InitTransferableObjects)
            {
                initTransferableObject.Init(_mOwner);
                _mActiveTransferableObjects.Add(initTransferableObject);
            }
        }
        private void PopulatePlayerTransferableObjects()
        {
            _mPlayerTransferableObjects.Clear();
            foreach (var initTransferableObject in initPlayerTransferableObjects)
            {
                initTransferableObject.Init(_mOwner);
                _mPlayerTransferableObjects.Add(initTransferableObject);
            }
        }
        public void CleanAllObjects()
        {
            CleanRowObjects();
        }
        private void CleanRowObjects()
        {
            _mActiveTransferableObjects.Clear();
        }
        public void UpdateSimpleTransferableObjectData()
        {
            Debug.Log($"Row: {RowIndex} is updating data");
            _mActiveTransferableObjects.Clear();
            for (var i = 0; i<GetRowTransform.childCount;i++)
            {
                if (GetRowTransform.GetChild(i).TryGetComponent<ISimpleTransferableObject>(out var transferableObject))
                {
                    if (transferableObject.CurrentFaceOwner != _mOwner)
                    {
                        Debug.LogWarning("Lvl Face Owner and Transferable Object owner must be the same at this point");
                        continue;
                    }
                    _mActiveTransferableObjects.Add(transferableObject);
                }
            }
            Debug.Log($"Active Transferable Objects in Row {RowIndex} for player {_mOwner} updated. {_mActiveTransferableObjects.Count} Current Objects");
        }
        public void UpdatePlayerBasedTransferableObjectsData()
        {
            Debug.Log($"Row: {RowIndex} is updating data");
            _mPlayerTransferableObjects.Clear();
            for (var i = 0; i<GetRowTransform.childCount;i++)
            {
                if (GetRowTransform.GetChild(i).TryGetComponent<IPlayerBasedTransferableObject>(out var playerBasedTransferableObject))
                {
                    if (playerBasedTransferableObject.CurrentFaceOwner != _mOwner)
                    {
                        Debug.LogWarning("Lvl Face Owner and Transferable Object owner must be the same at this point");
                        continue;
                    }
                    _mPlayerTransferableObjects.Add(playerBasedTransferableObject);
                }
            }
            Debug.Log($"Player-Based Transferable Objects in Row {RowIndex} for player {_mOwner} updated. {_mPlayerTransferableObjects.Count} Current Objects");
        }
        public Transform GetRowTransform => gameObject.transform;
    }
}