using System;
using LvlFacesManagement;
using UnityEngine;

namespace TransferObject
{
    public class TransferableObject : MonoBehaviour, ITransferableObject
    {
        private Vector2 _mStartPosition;
        private PlayerEnum _mCurrentLevelPlayerEnum;

        public Guid Id => _mId;
        private Guid _mId;
        
        private bool _mInitialized;
        public bool IsInit => _mInitialized;

        public void Initialize()
        {
            if (_mInitialized)
            {
                return;
            }
            _mStartPosition = transform.position;
            _mId = Guid.NewGuid();
            _mInitialized = true;
        }
    }
}