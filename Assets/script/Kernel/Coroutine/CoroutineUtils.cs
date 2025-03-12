using System.Collections;

namespace GoFire.Kernel
{
    public class CoroutineUtils
    {
        public static void WaitFor(IEnumerator enumerator)
        {
            while (enumerator.MoveNext())
            {
                
            }
        }
    }
}