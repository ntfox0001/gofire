using System.Collections;
using GoFire;
using UnityEngine;

namespace GoFire
{
    public class TestPackageProcedure : IProcedure
    {
        public IEnumerator Init(params object[] args)
        {
            var pg1 = new PackageGroup();
            yield return pg1.LoadPackage("Airplane");
            
            var pg2 = new PackageGroup();
            yield return pg2.LoadPackage("Airplane");

            yield return pg1.Release();

            var a = pg2.GetComponent<Airplane>("Aircraft01");
            

            a.transform.position = new Vector3(0, 0, 0);
        }

        public IEnumerator Release()
        {
            yield return null;
        }
    }
}