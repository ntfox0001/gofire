namespace GoFire
{
    public interface IInput
    {
        bool Bind(IMovable target);
        void Update();
        bool IsBind();
    }
}