using System.Collections;

namespace GoFire
{
    public interface IProcedure
    {
        IEnumerator Init(params object[] args);
        IEnumerator Release();
    }
}