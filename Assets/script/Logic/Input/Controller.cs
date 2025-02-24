using UnityEngine;

namespace GoFire
{
    public interface IController
    {
        bool Bind(GameObject target);
        void Update();
        bool IsBind();
    }
}