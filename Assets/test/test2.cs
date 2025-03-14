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
            yield return new WaitForObjectsEx(a1());
        }

        IEnumerator a3()
        {
            for (int i=0; i < 100; i++)
            {
                yield return null;
            }
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
            yield return new WaitForSeconds(1);
            Debug.Log(a + "-> a2_2");
            yield return new WaitForSeconds(1);
            Debug.Log(a + "-> a2_3");
        }
    }
}