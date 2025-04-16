namespace GoFire
{
    public enum DestroyStyle
    {
        Destroy, // 真的销毁
        Recycle, // 放入pool
    }
    public interface IDestroy
    {
        void OnWillDestroy(DestroyStyle style);
    }
}