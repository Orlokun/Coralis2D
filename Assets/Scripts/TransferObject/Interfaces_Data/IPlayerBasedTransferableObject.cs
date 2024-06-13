using LvlFacesManagement;
using TransferObject.Interfaces_Data;

namespace TransferObject
{
    public interface IPlayerBasedTransferableObject : ITransferableObject
    {
        public PlayerEnum InteractionOwner { get; }
    }
}