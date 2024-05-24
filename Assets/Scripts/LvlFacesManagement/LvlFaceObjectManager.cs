using System.Collections.Generic;
using TransferObject;
using UnityEngine;

namespace LvlFacesManagement
{
    public interface ILevelFaceObjectManager
    {
        public PlayerEnum PlayerOwner { get; }
        public void PopulateStartList();
    }
    public class LvlFaceObjectManager : MonoBehaviour, ILevelFaceObjectManager
    {
        public PlayerEnum PlayerOwner => _mPlayerFace;
        [SerializeField] private PlayerEnum _mPlayerFace;
    
        public List<ITransferableObject> ActiveTransferableObjects => _mActiveTransferableObjects;
        private List<ITransferableObject> _mActiveTransferableObjects = new();

        private void Awake()
        {
            
        }

        public void PopulateStartList()
        {
            _mActiveTransferableObjects.Clear();
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent<ITransferableObject>(out var transferableObject))
                {
                    if (!transferableObject.IsInit)
                    {
                        transferableObject.Initialize();
                    }
                    _mActiveTransferableObjects.Add(transferableObject);
                }
            }
        }

    }
}