namespace LvlFacesManagement
{
    public interface IInitialize<T>
    {
        public bool IsInit { get; }
        public void Init(T playerEnum);
    }

    public interface IInitialize
    {
        public bool IsInit { get; }
        public void Init();
    }
    

}