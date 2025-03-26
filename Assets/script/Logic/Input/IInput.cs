namespace GoFire
{
    public interface IInput
    {
        bool Bind(IMovable target);
        void Update(float deltaTime);
        bool IsBind();
    }
}