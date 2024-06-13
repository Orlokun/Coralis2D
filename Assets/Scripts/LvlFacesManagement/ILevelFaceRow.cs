using System.Collections.Generic;
using TransferObject;
using TransferObject.Interfaces_Data;
using UnityEngine;

namespace LvlFacesManagement
{
    public interface ILevelFaceRow : IInitialize<PlayerEnum>
    {
        public Transform GetRowTransform { get; }
        public int RowIndex { get; }
        void CleanAllObjects();
        void UpdateSimpleTransferableObjectData();
        void UpdatePlayerBasedTransferableObjectsData();
        public List<ISimpleTransferableObject> ActiveTransferableObjects { get; }
        public List<IPlayerBasedTransferableObject> PlayerTransferableObjects { get; }
    }
}