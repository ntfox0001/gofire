using System;

namespace GoFire
{
    public interface IInput
    {
        bool Bind(IMovable target, Action fire);
        void Update(float deltaTime);
        bool IsBind();
    }
}