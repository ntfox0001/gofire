namespace GoFire.Kernel
{
    public interface IDb
    {
        T Get<T>(string key);
        void Set<T>(string key, T value);
    }
}