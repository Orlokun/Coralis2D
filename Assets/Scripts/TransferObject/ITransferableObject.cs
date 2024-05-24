using System;

namespace TransferObject
{
    public interface ITransferableObject
    {
        public bool IsInit { get; }
        public void Initialize();
        public Guid Id { get; }
    }
}