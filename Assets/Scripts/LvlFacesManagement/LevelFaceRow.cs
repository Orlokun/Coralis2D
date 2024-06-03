using System;
using System.Collections.Generic;
using System.Linq;
using TransferObject;
using UnityEngine;

namespace LvlFacesManagement
{
    public interface ILevelFaceRow : IInitialize<PlayerEnum>
    {
        public Transform GetRowTransform { get; }
        public int RowIndex { get; }
        void CleanAllObjects();
        void UpdateObjectsData();
        public List<ITransferableObject> ActiveTransferableObjects { get; }
    }
    
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
        public List<ITransferableObject> ActiveTransferableObjects => _mActiveTransferableObjects;
        private List<ITransferableObject> _mActiveTransferableObjects = new();
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
            _mActiveTransferableObjects.Clear();
            foreach (var initTransferableObject in InitTransferableObjects)
            {
                initTransferableObject.Init(_mOwner);
                _mActiveTransferableObjects.Add(initTransferableObject);
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

        public void UpdateObjectsData()
        {
            Debug.Log($"Row: {RowIndex} is updating data");
            _mActiveTransferableObjects.Clear();
            for (var i = 0; i<GetRowTransform.childCount;i++)
            {
                if (GetRowTransform.GetChild(i).TryGetComponent<ITransferableObject>(out var transferableObject))
                {
                    if (transferableObject.CurrentOwner != _mOwner)
                    {
                        Debug.LogWarning("Lvl Face Owner and Transferable Object owner must be the same at this point");
                        continue;
                    }
                    _mActiveTransferableObjects.Add(transferableObject);
                }
            }
            Debug.Log($"Active Transferable Objects in Row {RowIndex} for player {_mOwner} updated. {_mActiveTransferableObjects.Count} Current Objects");
        }
        
        public Transform GetRowTransform => gameObject.transform;
    }
}