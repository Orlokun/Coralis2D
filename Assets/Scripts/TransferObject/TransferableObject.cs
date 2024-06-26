﻿using System;
using LvlFacesManagement;
using TransferObject.Interfaces_Data;
using UnityEngine;

namespace TransferObject
{
    public class TransferableObject : MonoBehaviour, ISimpleTransferableObject
    {
        private ITransferableObjectData _mBaseData;
        public Guid Id => _mBaseData.Id;
        public PlayerEnum CurrentFaceOwner => _mBaseData.CurrentFaceOwner;
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
            _mBaseData.SetNewFace(newOwner);
        }
    }
}