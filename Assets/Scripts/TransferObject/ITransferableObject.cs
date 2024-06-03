using System;
using LvlFacesManagement;
using UnityEngine;

namespace TransferObject
{
    public interface ITransferableObject : IInitialize<PlayerEnum>
    {
        public Guid Id { get; }
        public PlayerEnum CurrentOwner { get; }
        public void UpdatePosition(Transform newParent, PlayerEnum newOwner);
    }
}