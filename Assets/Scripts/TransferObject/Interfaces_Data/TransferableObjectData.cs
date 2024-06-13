using System;
using LvlFacesManagement;
using UnityEngine;

namespace TransferObject
{
    internal class TransferableObjectData : ITransferableObjectData
    {
        public Vector2 StartPosition => _mStartPosition;
        private Vector2 _mStartPosition;

        public PlayerEnum CurrentFaceOwner => _mCurrentPlayerOwner;
        private PlayerEnum _mCurrentPlayerOwner;
        private Guid _mId;

        public Guid Id => _mId;
        public void SetNewFace(PlayerEnum newOwner)
        {
            _mCurrentPlayerOwner = newOwner;
        }
        public TransferableObjectData(Vector2 mStartPosition, PlayerEnum mCurrentPlayerOwner, Guid mId)
        {
            _mStartPosition = mStartPosition;
            _mCurrentPlayerOwner = mCurrentPlayerOwner;
            _mId = mId;
        }
    }
}