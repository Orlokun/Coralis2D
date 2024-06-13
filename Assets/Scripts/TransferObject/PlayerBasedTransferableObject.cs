using System;
using LvlFacesManagement;
using UnityEngine;

namespace TransferObject
{
    internal class PlayerBasedTransferableObject : MonoBehaviour, IPlayerBasedTransferableObject
    {
        [SerializeField] private PlayerEnum mInteractionOwner;
        
        //Public Fields
        public Guid Id => _mBaseData.Id;

        public Vector2 StartPosition => _mBaseData.StartPosition;
        public PlayerEnum CurrentFaceOwner => _mBaseData.CurrentFaceOwner;
        public PlayerEnum InteractionOwner => mInteractionOwner;
        public bool IsInit => _mInitialized;

        //Private members
        private ITransferableObjectData _mBaseData;
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
            _mBaseData.SetNewFace(newOwner);
        }
    }
}