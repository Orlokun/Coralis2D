using System;
using LvlFacesManagement;
using UnityEngine;

namespace TransferObject
{
    internal interface ITransferableObjectData
    {
        public Guid Id { get; }
        public void SetNewFace(PlayerEnum newOwner);
        public Vector2 StartPosition { get; }
        public PlayerEnum CurrentFaceOwner { get; }
    }
}