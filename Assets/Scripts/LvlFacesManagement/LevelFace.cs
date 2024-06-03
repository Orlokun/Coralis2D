using System.Collections.Generic;
using TransferObject;
using UnityEngine;

namespace LvlFacesManagement
{
    public interface ILevelFace : IInitialize
    {
        public PlayerEnum PlayerOwner { get; }
        public void UpdateRowObjectsData(int row);
        public Transform GetRowParentTransform(int rowIndex);
        public List<ITransferableObject> GetRowObjects(int rowIndex);
        void ClearRow(int currentRow);
    }
    public class LevelFace : MonoBehaviour, ILevelFace
    {
        public PlayerEnum PlayerOwner => _mFaceOwner;
        [SerializeField] private PlayerEnum _mFaceOwner;
    
        public Dictionary<int, ILevelFaceRow> FaceRows => _mFaceRows;
        private Dictionary<int, ILevelFaceRow> _mFaceRows = new();
        
        
        [SerializeField] private List<LevelFaceRow> MFaceRows;

        public bool IsInit => _mIsInit;
        private bool _mIsInit;
        
        public void Init()
        {
            CheckBaseMembersAndProperties();
            if (_mIsInit)
            {
                return;
            }
            InitializeRows();
            _mIsInit = true;
        }

        private void InitializeRows()
        {
            for(var i =0; i<MFaceRows.Count;i++)
            {
                MFaceRows[i].Init(_mFaceOwner);
                _mFaceRows.Add(i+1, MFaceRows[i]);
            }
        }
        private void CheckBaseMembersAndProperties()
        {
            if (_mFaceOwner == 0)
            {
                Debug.LogError("[LevelFace.CheckBaseMembersAndProperties] Level Face must have a player owner assigned");
            }
            if (MFaceRows.Count == 0)
            {
                Debug.LogError("[LevelFace.CheckBaseMembersAndProperties] Level Face must have at least one row");
            }
        }
        
        public void UpdateRowObjectsData(int row)
        {
            if (!FaceRows.ContainsKey(row))
            {
                Debug.LogWarning("Row must be available for object transfer");
                return;
            }
            FaceRows[row].UpdateObjectsData();
        }
        
        public Transform GetRowParentTransform(int rowIndex)
        {
            if (!FaceRows.ContainsKey(rowIndex))
            {
                Debug.LogWarning("Row must be available for object transfer");
                return null;
            }
            return FaceRows[rowIndex].GetRowTransform;
        }

        public List<ITransferableObject> GetRowObjects(int rowIndex)
        {
            if (!FaceRows.ContainsKey(rowIndex))
            {
                return null;
            }
            return FaceRows[rowIndex].ActiveTransferableObjects;
        }

        public void ClearRow(int rowIndex)
        {
            if (!FaceRows.ContainsKey(rowIndex))
            {
                Debug.LogWarning("[LevelFace.GetRowObjects] This Row must be available upon request");
                return;
            }
            _mFaceRows[rowIndex].ActiveTransferableObjects.Clear();
        }
    }
}