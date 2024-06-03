using System;
using LvlFacesManagement;
using UnityEngine;

namespace TransferObject
{
    internal interface ITransferableObjectData
    {
        public Guid Id { get; }
        public void SetNewFaceOwner(PlayerEnum newOwner);
        public Vector2 StartPosition { get; }
        public PlayerEnum CurrentPlayerOwner { get; }
    }
    internal class TransferableObjectData : ITransferableObjectData
    {
        public Vector2 StartPosition => _mStartPosition;
        private Vector2 _mStartPosition;

        public PlayerEnum CurrentPlayerOwner => _mCurrentPlayerOwner;
        private PlayerEnum _mCurrentPlayerOwner;
        private Guid _mId;

        public Guid Id => _mId;
        public void SetNewFaceOwner(PlayerEnum newOwner)
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

    public class TransferableObject : MonoBehaviour, ITransferableObject
    {
        private ITransferableObjectData _mBaseData;
        public Guid Id => _mBaseData.Id;
        public PlayerEnum CurrentOwner => _mBaseData.CurrentPlayerOwner;
        public bool IsInit => _mInitialized;

        private bool _mInitialized;

        public void Init(PlayerEnum playerEnum)
        {
            if (_mInitialized)
            {
                return;
            }
            _mBaseData = new TransferableObjectData(transform.localPosition, playerEnum, Guid.NewGuid());
            _mInitialized = true;
        }

        public void UpdatePosition(Transform newParent, PlayerEnum newOwner)
        {
            transform.SetParent(newParent);
            transform.SetLocalPositionAndRotation(_mBaseData.StartPosition, transform.rotation);
            _mBaseData.SetNewFaceOwner(newOwner);
        }
    }
}