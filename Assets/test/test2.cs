using System.Collections;
using GoFire.Kernel;
using UnityEngine;

namespace test
{
    public class test2 : MonoBehaviour
    {
        public void Click()
        {
            Debug.Log("click");

            StartCoroutine(ss());
        }

        IEnumerator ss()
        {
            // yield return new WaitForObjectsEx(a1());
            var ie = b2();
            yield return CoroutineUtils.WaitForProgress(ie, (p) =>
            {
                Debug.Log(p);
            });
        }

        IEnumerator b2()
        {
            yield return new WaitForTime(10);
        }
        IEnumerator b1()
        {
            var ies = new IEnumerator[2];
            ies[0] = a1();
            ies[1] = a2("222");
            yield return new WaitForObjectsEx(ies);
        }
        IEnumerator a1()
        {
            Debug.Log("a1_1");
            yield return a2("iii");
            Debug.Log("a1_2");
            yield return a2("jjj");
            Debug.Log("a1_3");
        }
        
        IEnumerator a2(string a)
        {
            Debug.Log(a + "-> a2_1");
            yield return new WaitForTime(1);
            Debug.Log(a + "-> a2_2");
            yield return new WaitForTime(1);
            Debug.Log(a + "-> a2_3");
        }
    }
}