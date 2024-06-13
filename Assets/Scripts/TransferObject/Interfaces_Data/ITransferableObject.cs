using System;
using LvlFacesManagement;
using UnityEngine;

namespace TransferObject.Interfaces_Data
{
    public interface ITransferableObject : IInitialize<PlayerEnum>
    {
        public Guid Id { get; }
        public PlayerEnum CurrentFaceOwner { get; }
        public void UpdatePosition(Transform newParent, PlayerEnum newOwner);
    }
}