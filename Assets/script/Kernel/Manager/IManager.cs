using System.Collections;

namespace GoFire.Kernel
{
    public interface IManager
    {
        IEnumerator Init();
        void Release();
    }
}